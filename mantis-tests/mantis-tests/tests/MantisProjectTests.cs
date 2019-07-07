using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using NUnit.Framework;

namespace mantis_tests
{
    [TestFixture]
    public class MantisProjectTests : TestBase

    {
        [SetUp]

        public void LoginAsAdmin()
        {
            AccountData admin = new AccountData
            {
                Name = "administrator",
                Password = "root"
            };

            app.Registration.OpenMainPage();
            app.Registration.FillAuthForm(admin);
            app.Registration.SubmitOneButtonForm();
        }

        [Test]
        public void MantisProjectAdding()
        {
            List<ProjectData> oldprojects = new List<ProjectData>();
            oldprojects = app.Project.GetProjectList();

            ProjectData project = new ProjectData
            {
                Name = "112",
                Description = "112"
            };

            app.Project.AddMantisProject(project);

            oldprojects.Add(project);
            List<ProjectData> newprojects = app.Project.GetProjectList();

            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldprojects, newprojects);

        }

        [Test]
        public void MantisProjectRemoving()
        {
            int N = 2;
            List<ProjectData> oldprojects = new List<ProjectData>();
            oldprojects = app.Project.GetProjectList();

            ProjectData removedProject = oldprojects[N];

            app.Project.RemoveMantisProject(N);

            oldprojects.Remove(removedProject);
            List<ProjectData> newprojects = app.Project.GetProjectList();

            oldprojects.Sort();
            newprojects.Sort();
            Assert.AreEqual(oldprojects, newprojects);
        }

        [TearDown]

        public void Loguot()
        {
            app.Registration.InitLogOut();
        }
    }
}
