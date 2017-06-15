using Entities;
using Nelibur.ObjectMapper;
using PrimaryHub.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using DaEntities = DataEntitiesAcces.CommonEntities;
using System.Data.Entity;

namespace PrimaryHub
{
    public partial class Hub
    {

        public GetGroupsOut GetGroups(GetGroupsIn input)
        {
            var response = new GetGroupsOut();
            try
            {
                using (var db = new DataEntitiesAcces.db())
                {


                }
            }
            catch (Exception ex)
            {
                response.result = ex.Message;
            }

            return response;
        }

        public RemoveGroupOut RemoveGroup(RemoveGroupIn input)
        {
            var response = new RemoveGroupOut();

            return response;
        }

        public AddGroupOut AddGroup(AddGroupIn input)
        {
            var response = new AddGroupOut();

            return response;
        }


        DaEntities.Group GroupMapper(Group group)
        {
            DaEntities.Group result;
            TinyMapper.Bind<Group, DaEntities.Group>(config =>
            {
                config.Ignore(g => g.snippets);
            });
            result = TinyMapper.Map<DaEntities.Group>(group);
            return result;
        }

    }
}
