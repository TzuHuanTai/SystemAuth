using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemAuth.Models;

namespace SystemAuth.ViewModels
{
    public class vmIRole
    {
    }

    public class MenuNode
    {
        public string MenuText { get; set; }
        public int MenuId { get; set; }
        public List<MenuNode> Children { get; set; }
    }

    public class CtrlActionNode
    {
        public int Id { get; set; }
        public string Name { get; set; }        
        public List<ActionNode> Children { get; set; }
    }

	public class ActionNode
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Method { get; set; }
		public int ControllerId { get; set; }
		public string Description { get; set; }		
	}

	public class RoleGroupNode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<RoleGroupNode> Children { get; set; }
    }
}
