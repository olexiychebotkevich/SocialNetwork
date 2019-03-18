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
   public class GroupService : IGroupService
    {


        IUnitOfWork Database { get; set; }

       

        public GroupService(IUnitOfWork uow)
        {
            Database = uow;
        }


        public List<GroupDTO> GetGroups()
        {

            //List<PostDTO> posts = null;

            //List<UserDTO> subscribers = null;

            //List < GroupDTO > groupsdto= null;

            //List<Group> groups;
            //try
            //{
            //    groups = Database.GroupManager.GetAllGroups();

            //    //foreach(Group group in groups)
            //    //foreach (var p in group.Posts)
            //    //{
            //    //    posts.Add(new PostDTO
            //    //    {
            //    //        Author = p.Author,
            //    //        Subject = p.Subject,
            //    //        Date = p.Date,
            //    //        Photos = p.Photos,
            //    //        Text = p.Text
            //    //    }
            //    //   );
            //    //}

            //    //foreach (Group group in groups)
            //    //foreach (var s in group.Subscribers)
            //    //{
            //    //    subscribers.Add(new UserDTO
            //    //    {
            //    //        Age = s.Age,

            //    //        Country = s.Country,
            //    //        Email = s.Email,
            //    //        UserName = s.Name
            //    //    });
            //    //}


            //    foreach (var g in groups )
            //    {
            //        groupsdto.Add(new GroupDTO
            //        {
            //           Name=g.Name,
            //           Description=g.Description,
            //           Posts=posts,


            //        });
            //    }
            //}
            //catch
            //{
            //    return groupsdto;
            //}

            //return groupsdto;

            List<GroupDTO> groupDTOs = new List<GroupDTO>();
            foreach(var g in Database.GroupManager.GetAllGroups())
            {
                groupDTOs.Add(new GroupDTO
                {
                    Name = g.Name,
                    Description = g.Description,
                    MainImage = g.MainImage
                });
            }

            return groupDTOs;

        }

        public GroupDTO GetGroup(string Name)
        {
            GroupDTO groupDTO = null;

            List<PostDTO> posts=null;

            List<UserDTO> subscribers = null;

            Group group = Database.GroupManager.GetGroupByName(Name);
            if(group!=null)
            {
                foreach(var p in group.Posts)
                {
                    posts.Add(new PostDTO
                    {
                        Author = p.Author,
                        Subject = p.Subject,
                        Date = p.Date,
                        Photo = p.Photo,
                        Text = p.Text
                    }
                   );
                }

                foreach( var s in group.Subscribers )
                {
                    subscribers.Add(new UserDTO
                    {
                        Age = s.Age,
                       
                        Country = s.Country,
                        Email = s.Email,
                        UserName = s.Name
                    });
                }

                groupDTO = new GroupDTO
                {
                    Name = group.Name,

                    Description = group.Description,

                    Posts = posts,

                    Subscribers = subscribers,

                    MainImage=group.MainImage

                };
               
            }

            return groupDTO;
        }

        public OperationDetails Create(GroupDTO grouprDto)
        {
            Group group = Database.GroupManager.GetGroupByName(grouprDto.Name);
            if(group==null)
            {
                group = new Group { Name = grouprDto.Name, Description = grouprDto.Description,MainImage=grouprDto.MainImage};
                Database.GroupManager.Create(group);
                return new OperationDetails(true, "Group added", "");
            }
            else
            {
                return new OperationDetails(false, "A group with the same name already exists", "Name");
            }


           
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
