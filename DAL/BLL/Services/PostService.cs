using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PostService:IPostService
    {

        IUnitOfWork Database { get; set; }



        public PostService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public OperationDetails Create(PostDTO postDto)
        {



            try
            {
                Database.PostManager.Create(new Post { Author = postDto.Author, Date = postDto.Date, Subject = postDto.Subject, Photos = postDto.Photos, Text = postDto.Text });
                return new OperationDetails(true, "Group added", "");
            }
            catch
            {
                return new OperationDetails(false, "Group not added", "Group");
            }

            
        }

        public List<PostDTO> GetAllPosts()
        {
            List<PostDTO> postDTOs = null;
            foreach(var p in Database.PostManager.GetAllPosts())
            {
                postDTOs.Add
                    (
                    new PostDTO
                    {
                        Author=p.Author,
                        Subject=p.Subject,
                        Date=p.Date,
                        Photos=p.Photos,
                        Text=p.Text
                    }
                    );
            }


            return postDTOs;
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
