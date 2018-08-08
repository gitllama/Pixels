using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFViewer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Add
        public static Autofac.IContainer modelcontainer;
        #endregion

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            #region Add

            var modelbuilder = new ContainerBuilder();
            modelbuilder.RegisterInstance(Models.ModelBilder.Build()).SingleInstance();
            modelcontainer = modelbuilder.Build();

            #endregion

            var bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }
    }
}
