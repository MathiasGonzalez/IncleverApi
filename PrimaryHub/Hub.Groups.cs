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

                    response.Groups = db.Groups.Where(grp => grp.groupid == 2).Select(grp => GroupMapper(grp)).ToList();

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

            if (group.snippets != null)
            {
                result.snippets = group.snippets.Select(s => SnippetMapper(s)).ToList();
            }
            return result;
        }

        Group GroupMapper(DaEntities.Group group)
        {
            Group result;
            TinyMapper.Bind<DaEntities.Group, Group>(config =>
             {
                 config.Ignore(g => g.snippets);
             });
            result = TinyMapper.Map<Group>(group);

            if (group.snippets != null)
            {
                result.snippets = group.snippets.Select(s => SnippetMapper(s)).ToList();
            }

            return result;
        }

    }
}
