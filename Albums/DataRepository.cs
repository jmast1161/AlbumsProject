using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Albums.Models;

namespace Albums
{
    public class DataRepository
    {
        public List<AlbumEntryData> AlbumEntries { get; private set; }
        public DataRepository()
        {
            var webClient = new WebClient();
            var userJson = webClient.DownloadString(@"https://jsonplaceholder.typicode.com/users");
            var photosJson = webClient.DownloadString(@"https://jsonplaceholder.typicode.com/photos");
            var albumsJson = webClient.DownloadString(@"https://jsonplaceholder.typicode.com/albums");
            var postsJson = webClient.DownloadString(@"https://jsonplaceholder.typicode.com/posts");

            var userData = JsonConvert.DeserializeObject<List<UserData>>(userJson);
            var photoData = JsonConvert.DeserializeObject<List<PhotoData>>(photosJson);
            var albumData = JsonConvert.DeserializeObject<List<AlbumData>>(albumsJson);
            var postData = JsonConvert.DeserializeObject<List<PostData>>(postsJson);

            AlbumEntries = new List<AlbumEntryData>();

            // not making any assumptions as to data ids guaranteed in ascending order
            // otherwise would track index to avoid iterating entire list of albums
            foreach (var user in userData)
            {
                foreach (var album in albumData)
                {
                    if (user.Id.Equals(album.UserId))
                    {
                        var albumEntry = new AlbumEntryData();
                        albumEntry.AlbumThumbnails = new List<AlbumThumbnailData>();

                        var albumThumbnails = new List<AlbumThumbnailData>();
                        foreach (var photo in photoData)
                        {
                            if (photo.AlbumId == album.Id)
                            {
                                albumThumbnails.Add(new AlbumThumbnailData()
                                {
                                    AlbumThumbnailPath = photo.ThumbnailUrl,
                                    AlbumPhotoTitle = photo.Title,
                                });
                            }
                        }

                        var posts = new List<PostData>();
                        foreach (var post in postData)
                        {
                            if (post.UserId == user.Id)
                            {
                                posts.Add(post);
                            }
                        }

                        albumEntry.AlbumThumbnails.AddRange(albumThumbnails);
                        albumEntry.AlbumTitle = album.Title;
                        albumEntry.UserInfo = user;
                        albumEntry.Posts = posts;

                        AlbumEntries.Add(albumEntry);
                    }
                }
            }
        }
    }
}
