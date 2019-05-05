import Foundation
import UIKit
import Photos

@objc public class SwiftForUnity: UIViewController {
    
    @objc static let shared = SwiftForUnity()
    @objc func SayHiToUnity() -> String{
        
        let group = DispatchGroup()
        
        let videosArray = PHAsset.fetchAssets(with: .video, options: nil)
        
        var result:String = ""
        
        for index in 0..<videosArray.count {
            
            group.enter()
            
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
                
                result = result + expression1 + "@" + expression2 + ","
                
                group.leave()
            })
        }
        
        group.wait()
        print("Final "+result)
        return result
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
}
