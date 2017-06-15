using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimaryHub.Parameters
{
    #region GetGroups
    public class GetGroupsIn : BaseParameter
    {

    }
    public class GetGroupsOut : BaseParameter
    {
        public List<Group> Groups { get; set; }
    }
    #endregion

    #region AddGroup
    public class AddGroupIn : BaseParameter
    {
        public Group Group { get; set; }
    }
    public class AddGroupOut : BaseParameter
    {
        public Group Group { get; set; }
    }
    #endregion

    #region RemoveGroup
    public class RemoveGroupIn : BaseParameter
    {
        public Group Group { get; set; }
    }
    public class RemoveGroupOut : BaseParameter
    {

    }
    #endregion
}
