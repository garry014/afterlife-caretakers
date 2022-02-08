using afterlife_caretakers.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace afterlife_caretakers.Services
{
    public class VideoMemoService
    {
        private Models.ALCDBContext _context;
        public VideoMemoService(Models.ALCDBContext context)
        {
            _context = context;
        }

        public bool AddVideo(Video newvideo)
        {
            
            if (VideoExists(newvideo.Id))
            {
                return false;
            }
            _context.Add(newvideo);
            _context.SaveChanges();
            return true;
        }
        public List<Video> GetAllVideos()
        {
            List<Video> AllVideos = new List<Video>();
            AllVideos = _context.VideoMemo.ToList();
            return AllVideos;
        }
        public Video GetVideoById(int id)
        {
            Video theVideo = _context.VideoMemo.Where(c => c.Id == id).FirstOrDefault();
            return theVideo;
        }
        public Video GetVideoByWillMakerId(int id)
        {
            Video theVideo = _context.VideoMemo.Where(c => c.willMakerID == id).FirstOrDefault();
            return theVideo;
        }
        private bool VideoExists(int id)
        {
            return _context.VideoMemo.Any(c => c.Id == id);
        }

        public bool UpdateVideo(Video theVideo)
        {
            bool updated = true;
            _context.Attach(theVideo).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(theVideo.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;


        }

        // Soft delete
        public bool DeleteCasket(Video theVideo)
        {
            bool updated = true;
            _context.Attach(theVideo).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
                updated = true;

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(theVideo.Id))
                {
                    updated = false;
                }
                else
                {
                    throw;
                }
            }
            return updated;


        }
    }
}
