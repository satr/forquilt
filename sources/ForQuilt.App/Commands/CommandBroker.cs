//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ForQuilt.App.Commands.WorkArea;

namespace ForQuilt.App.Commands
{
    static class CommandBroker
    {
        public enum CommandType
        {
            Editing,
            AlwaysEnabled,
            Unspecified,
        }

        static readonly IDictionary<CommandType, ICollection<RegisteredCommandBase>> RegisteredCommands = new Dictionary<CommandType, ICollection<RegisteredCommandBase>>();

        public static void Register(RegisteredCommandBase command, CommandType commandType)
        {
            if (!RegisteredCommands.ContainsKey(commandType))
            {
                RegisteredCommands.Add(commandType, new Collection<RegisteredCommandBase>());
            }
            RegisteredCommands[commandType].Add(command);
        }

        public static void BreakCurrentEditingCommand(WorkAreaEditingCommandBase executingCommand)
        {
            foreach (var command in GetCommandsFor(CommandType.Editing)
                                    .OfType<WorkAreaEditingCommandBase>()
                                    .Where(command => command != executingCommand))
            {
                command.Break();
            }
        }

        private static IEnumerable<RegisteredCommandBase> GetCommandsFor(CommandType commandType)
        {
            return RegisteredCommands.ContainsKey(commandType) 
                ? RegisteredCommands[commandType] 
                : new Collection<RegisteredCommandBase>();
        }

        public static void BreakAllEditingCommands()
        {
            BreakCurrentEditingCommand(null);
        }
    }
}
