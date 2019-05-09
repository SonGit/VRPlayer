import Foundation
import UIKit
import Photos

@objc public class SwiftForUnity: UIViewController {
    
    @objc static let shared = SwiftForUnity()
    @objc func SayHiToUnity() -> String{
        
        // let group = DispatchGroup()
        
        let videosArray = PHAsset.fetchAssets(with: .video, options: nil)
        
        for index in 0..<videosArray.count {
            
            //group.enter()
            
            videosArray[index].getURL(completionHandler: { (URL,length) in
                
                let fileName = (videosArray[index].originalFilename)!
                let urlString = (URL?.absoluteString)!
                
                let resources = PHAssetResource.assetResources(for: videosArray[index])
                
                var sizeOnDisk: Int64? = 0
                
                if let resource = resources.first
                {
                    let unsignedInt64 = resource.value(forKey: "fileSize") as? CLong
                    sizeOnDisk = Int64(bitPattern: UInt64(unsignedInt64!))
                }
                let fileSize = (sizeOnDisk?.description)!
                
                let creationDate = (videosArray[index].creationDate?.description)!
                
                let lengthTime = (length?.seconds)!.description
                
                let expression1 = fileName + "@" + urlString + "@" + lengthTime
                
                let expression2 = fileSize + "@" + creationDate
                
                let result = expression1 + "@" + expression2
                
                self.startScreenCapture(message:result)
                // group.leave()
            })
        }
        
        for index in 0..<videosArray.count {
            
            videosArray[index].buildThumbnail ()
        }
        
        
        return "Nothing here!"
    }
    
    let kCallbackTarget = "LocalVideoManager"
    
    @objc func startScreenCapture( message:String ) {
        UnitySendMessage(kCallbackTarget, "BuildLocalVideo", message)
    }
}

extension PHAsset {
    var originalFilename: String? {
        return PHAssetResource.assetResources(for: self).first?.originalFilename
    }
    
    func getURL(completionHandler : @escaping ((_ responseURL : URL?,_ length: CMTime?) -> Void)){
        if self.mediaType == .video {
            
            let options: PHVideoRequestOptions = PHVideoRequestOptions()
            options.version = .original
            PHImageManager.default().requestAVAsset(forVideo: self, options: options, resultHandler: {(asset: AVAsset?, audioMix: AVAudioMix?, info: [AnyHashable : Any]?) -> Void in
                if let urlAsset = asset as? AVURLAsset {
                    let localVideoUrl: URL = urlAsset.url as URL
                    let length: CMTime = urlAsset.duration
                    
                    completionHandler(localVideoUrl,length)
                } else {
                    completionHandler(nil,nil)
                }
            })
        }
    }
    
    func buildThumbnail(){
        if self.mediaType == .video {
            
            let options: PHVideoRequestOptions = PHVideoRequestOptions()
            options.version = .original
            PHImageManager.default().requestAVAsset(forVideo: self, options: options, resultHandler: {(asset: AVAsset?, audioMix: AVAudioMix?, info: [AnyHashable : Any]?) -> Void in
                if let urlAsset = asset as? AVURLAsset {
                    
                    let path = (NSSearchPathForDirectoriesInDomains(.documentDirectory, .userDomainMask, true)[0] as NSString).appendingPathComponent("localTemp")
                    
                    let url = NSURL(string: path)
                    
                    let fileManager = FileManager.default
                    
                    let imagePath = url!.appendingPathComponent(self.originalFilename!)
                    
                    let urlString: String = imagePath!.absoluteString
                    
                    if !fileManager.fileExists(atPath: urlString) {
                        
                        let generator = AVAssetImageGenerator(asset: urlAsset)
                        generator.appliesPreferredTrackTransform = true
                        generator.maximumSize = CGSize(width: 320, height: 320)
                        
                        let timestamp = CMTime(seconds: 0, preferredTimescale: 60)
                        
                        if let imageRef = try? generator.copyCGImage(at: timestamp, actualTime: nil) {
                            //return UIImage(cgImage: imageRef)
                            let image = UIImage(cgImage: imageRef);
                            
                            if !fileManager.fileExists(atPath: path) {
                                try! fileManager.createDirectory(atPath: path, withIntermediateDirectories: true, attributes: nil)
                                
                            }
                            
                            let imageData = UIImageJPEGRepresentation(image,0.15)
                            
                            print("building thumbnail at  " + urlString);
                            
                            fileManager.createFile(atPath: urlString as String, contents: imageData, attributes: nil)
                            
                        } else {
                            //return nil
                        }
                        
                    } else
                    {
                        print("thumbnail already exists!  " + urlString);
                        
                    }
                    
                } else {
                    
                }
            })
        }
    }
}
