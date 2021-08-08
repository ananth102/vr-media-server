package com.ananth.vrbackend.vrbackend;

import java.io.IOException;
import java.net.MalformedURLException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.annotation.XmlRootElement;

import org.springframework.beans.factory.annotation.Value;
import org.springframework.core.io.UrlResource;
import org.springframework.core.io.support.ResourceRegion;
import org.springframework.http.HttpHeaders;
import org.springframework.http.HttpRange;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.MediaTypeFactory;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RestController;

@RestController
@CrossOrigin
public class VideoController{


    @GetMapping("/")
    public String home(){
        return "woof";
    }

    @Value("${video.location}")
    private String pathName;


    @GetMapping("/videos/{Videoname}")
    public ResponseEntity<ResourceRegion> getVideo(@PathVariable String Videoname,
    @RequestHeader HttpHeaders headers){
        try {
            UrlResource vid = new UrlResource("file:"+pathName+"/"+Videoname);
            ResourceRegion r = getResourceRegion(vid, headers);
            return ResponseEntity.status(HttpStatus.PARTIAL_CONTENT).
                   contentType(MediaTypeFactory.
                   getMediaType(vid).orElse(MediaType.APPLICATION_OCTET_STREAM)).body(r);
        } catch (MalformedURLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return null;
    }

    private ResourceRegion getResourceRegion(UrlResource video,HttpHeaders headers){
        try {
            long contentLength = video.contentLength();
            // headers.getRange
            List<HttpRange> range =  headers.getRange();
            if (range != null && range.size() != 0){
                long start = range.get(0).getRangeStart(contentLength);
                long end = range.get(0).getRangeEnd(contentLength);
                long rangeLength = Math.min(1 * 1024 * 1024, end - start + 1);
                return new ResourceRegion(video, start, rangeLength);
            }else{
                long rangeLength = Math.min(1 * 1024 * 1024, contentLength);
                return new ResourceRegion(video, 0, rangeLength);
            }
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return null;
    }

    @GetMapping("/getVideoList")
    public Object[] getVideoList(){
        ArrayList<String> woof = new ArrayList<String>();
        try {
            Files.list(Paths.get(pathName)).forEach(path -> woof.add(path.toString()));
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        // System.out.println(woof);
        // ResposeList resp = new ResposeList();
        // resp.setList(woof);
        return woof.toArray();
    }

    @XmlRootElement(name = "responseList")
    class ResposeList {

        private List<String> list;

        public List<String> getList() {
            return list;
        }

        public void setList(List<String> list) {
            this.list = list;
        }

    }

}


