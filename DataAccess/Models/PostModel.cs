using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models;

public class PostModel
{
    public int Id { get; set; }

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string UserId { get; set; }
    public string Thumbnail { get; set; }



}


