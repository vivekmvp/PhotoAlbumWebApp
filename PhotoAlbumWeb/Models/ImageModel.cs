using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoAlbumWeb.Models
{
    public class ImageModel
    {
        public List<string> ImageList { get; set; } = new List<string>();

        public List<IFormFile> Images { get; set; }
    }
}
