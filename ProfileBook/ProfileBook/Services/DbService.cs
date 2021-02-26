using ProfileBook;
using ProfileBook.Services.Repository;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProfileBook.Models;
namespace ProfileBook
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class DbService : ContentPage
    {
        string dbPath;

        public DbService()
        {
            dbPath = DependencyService.Get<IPath>().GetDatabasePath(App.DBFILENAME);
        }
        private void SavePerson(object sender, EventArgs e)
        {
            var person = (Person)BindingContext;
            if (!String.IsNullOrEmpty(person.Name))
            {
                using (ApplicationContext db = new ApplicationContext(dbPath))
                {
                    if (person.Id == 0)
                        db.Persons.Add(person);
                    else
                    {
                        db.Persons.Update(person);
                    }
                    db.SaveChanges();
                }
            }
            this.Navigation.PopAsync();
        }
        private void DeletePerson(object sender, EventArgs e)
        {
            var person = (Person)BindingContext;
            using (ApplicationContext db = new ApplicationContext(dbPath))
            {
                db.Persons.Remove(person);
                db.SaveChanges();
            }
            this.Navigation.PopAsync();
        }
    }
}