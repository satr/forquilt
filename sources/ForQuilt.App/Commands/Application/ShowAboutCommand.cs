//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using ForQuilt.App.Views;

namespace ForQuilt.App.Commands.Application
{
    class ShowAboutCommand : ShowViewCommand<AboutView>
    {
        public ShowAboutCommand(): base(CommandBroker.CommandType.AlwaysEnabled)
        {
        }
    }
}