﻿namespace API.Dtos.PhotoDtos
{
    public class GetUserPhotoDto
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Body { get; set; }
        public int LikesCount { get; set; }
    }
}
