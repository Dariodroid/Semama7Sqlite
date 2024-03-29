﻿using DeberSemana7Sqlite.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeberSemama7Sqlite
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConsultaRegistro : ContentPage
    {
        private SQLiteAsyncConnection con;
        private ObservableCollection<Estudiante> tablaEstudiantes;
        public ConsultaRegistro()
        {
            InitializeComponent();
            con = DependencyService.Get<Database>().GetConnection();
            get();
        }
        public async void get()
        {
            try
            {
                var Resultado = await con.Table<Estudiante>().ToListAsync();
                tablaEstudiantes = new ObservableCollection<Estudiante>(Resultado);
                listaUsuario.ItemsSource = tablaEstudiantes;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        private void listaUsuario_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Estudiante)e.SelectedItem; ;
            var item = obj.Id.ToString();
            var ID = Convert.ToInt32(item);
            var nombreItem = obj.Nombre;
            string nombre = nombreItem.ToString();
            var usuarioItem = obj.Usuario;
            string usuario = usuarioItem.ToString();
            var contrasenaItem = obj.Contrasena;
            string contrasena = contrasenaItem.ToString();

            Navigation.PushAsync(new Elemento(ID, nombre, usuario, contrasena));
        }
    }
}