using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetCagnotte.Application.DTOs
{
    public class UploadedFileDto
    {
        public Stream Content { get; set; } = Stream.Null;

        public string FileName { get; set; } = string.Empty;

        public string ContentType { get; set; } = string.Empty;
    }
}
