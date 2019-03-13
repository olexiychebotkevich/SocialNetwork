using BLL.DTO;
using BLL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
   public interface IPostService:IDisposable
    {

        OperationDetails Create(PostDTO postDto);

        List<PostDTO> GetAllPosts();

    }
}
