using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data
{
	public class SqlCommandAPIRepo : ICommandAPIRepo
	{
		
		private readonly CommandContext _context;
		
		public SqlCommandAPIRepo(CommandContext context){
			_context=context;
		}
		
		public void CreateCommand(Command cmd)
		{
			throw new System.NotImplementedException();
		}
		
		public void DeleteCommand(Command cmd)
		{
			//throw new System.NotImplementedException();

			if(cmd == null)
			{
				throw new ArgumentNullException(nameof(cmd));
			}

			_context.CommandItems.Remove(cmd);
		}
		
		public IEnumerable<Command> GetAllCommands()
		{
			return _context.CommandItems.ToList();
		}
		
		public Command GetCommandById(int id)
		{
			return _context.CommandItems.FirstOrDefault(p => p.Id==id);
		}
		
		public bool SaveChanges()
		{
			//throw new System.NotImplementedException();

			return (_context.SaveChanges() >= 0);
		}
		
		public void UpdateCommand(Command cmd)
		{
			//throw new System.NotImplementedException();
		}
	}
}