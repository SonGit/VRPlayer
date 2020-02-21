using com.ootii.Messages;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VRUdon.VR;

public class LocalVideoManager : MonoBehaviour
{
    public List<Video> localVideos = new List<Video>();

    void Awake()
    {
        MessageDispatcher.AddListener(GameEvent.showLocalVideoScreen, OnLoadLocalVideos);
    }

    void OnLoadLocalVideos(IMessage rMessage)
    {
        MessageDispatcher.SendMessageData(GameEvent.InitScreen, null);

        StartCoroutine(LoadProgress());
    }

    IEnumerator LoadProgress()
    {

        List<string> AllFolders = new List<string>();
        string origin = "D:\\Video";
#if UNITY_EDITOR

        try
        {
            var tempFolders = Directory.GetDirectories(origin);

            foreach (var folder in tempFolders)
            {
                string fileName = Path.GetFileName(folder);

                AllFolders.Add(folder);
            }

            for (int i = 0; i < AllFolders.Count; i++)
            {
                var paths = GetFileList("*.mp4", AllFolders[i]);
                foreach (var path in paths)
                {
                    LocalVideo localVideo = new LocalVideo(path);
                    localVideos.Add(localVideo);
                }
            }

            MessageDispatcher.SendMessageData(GameEvent.sendVideos, localVideos, EnumMessageDelay.NEXT_UPDATE);

        }
        catch (System.Exception e)
        {
            Debug.Log("LocalVideoManager Exception! " + e.Message);
        }
        finally
        {


        }

#endif
        yield return new WaitForEndOfFrame();
    }

    public static IEnumerable<string> GetFileList(string fileSearchPattern, string rootFolderPath)
    {
        Queue<string> pending = new Queue<string>();
        pending.Enqueue(rootFolderPath);
        string[] tmp;
        while (pending.Count > 0)
        {
            rootFolderPath = pending.Dequeue();
            try
            {
                tmp = Directory.GetFiles(rootFolderPath, fileSearchPattern);
            }
            catch (System.UnauthorizedAccessException)
            {
                continue;
            }
            for (int i = 0; i < tmp.Length; i++)
            {
                yield return tmp[i];
            }
            tmp = Directory.GetDirectories(rootFolderPath);
            for (int i = 0; i < tmp.Length; i++)
            {
                pending.Enqueue(tmp[i]);
            }
        }
    }
}
