using System;
using System.Collections.Generic;
using System.DirectoryServices;

namespace Jmcarrasc0.ActiveDirectory.Consult
{

    public class ActiveDirectory
    {
        /// <summary>
        /// Clase con los datos que devuelve la consulta del ad
        /// </summary>
        public class UserAd
        {
            /// <summary>
            /// Numero o ID del Empleado
            /// </summary>
            public int? EmployeeId { get; set; }

            /// <summary>
            /// Nombre de Usuario
            /// </summary>
            public string UserName { get; set; }
            /// <summary>
            /// Nombre
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Apellido
            /// </summary>
            public string LastName { get; set; }
            /// <summary>
            /// Cargo
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// Descripcion
            /// </summary>
            public string Description { get; set; }
            /// <summary>
            /// Departamento
            /// </summary>
            public string Department { get; set; }
            /// <summary>
            /// Compañia
            /// </summary>
            public string Company { get; set; }
            /// <summary>
            /// Estado
            /// </summary>
            public string State { get; set; }
            /// <summary>
            /// Ciudad
            /// </summary>
            public string City { get; set; }
            /// <summary>
            /// Correo
            /// </summary>
            public string Email { get; set; }
            /// <summary>
            /// Imagen o foto 
            /// </summary>
            public string Foto { get; set; }
            /// <summary>
            /// Oficina 
            /// </summary>
            public string Officce { get; set; }
            /// <summary>
            /// Display Name
            /// </summary>
            public string DisplayName { get; set; }
            /// <summary>
            /// Numero de telefono
            /// </summary>
            public string TelephoneNumber { get; set; }

            public string HomePhone { get; set; }

            public string Mobile { get; set; }

            public string Initials { get; set; }

            public string Manager { get; set; }

            public string StreetAddress { get; set; }

            public string PostalCode { get; set; }

            public string Country { get; set; }

            public string Sid { get; set; }

            public string Domain { get; set; }

        }

        private string GetProperty(SearchResult searchResult, string propertyName)
        {
            if (searchResult.Properties.Contains(propertyName))
            {
                return searchResult.Properties[propertyName][0].ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// obtener el usuario en el active directory 
        /// </summary>
        /// <param name="usrname">nombre de usurio ejemplo: jrojas</param>
        /// <param name="ldap"> LDAP ejemplo: LDAP://ve.kworld.cosa.com/DC=ve,DC=kworld,DC=kpmg,DC=com </param>
        /// <returns>retorna un objeto de tipo user Ad</returns>
        public UserAd GetUserActiveDirectorybyUsrName(string usrname, string ldap)
        {
            UserAd usuarios;
            var path = ldap.Trim();
            DirectoryEntry directory = new DirectoryEntry(path);
            DirectorySearcher mibusqueda = new DirectorySearcher(directory)
            {
                Filter =
                    $"(&((&(objectCategory=Person)(objectClass=User)))(samaccountname={usrname}))"
            };

            //mibusqueda.PropertiesToLoad.Add("cn"); mibusqueda.SearchScope = SearchScope.Subtree;

            // obtiene los resultados de la Busqueda 
            SearchResult rpdbusqueda = mibusqueda.FindOne();
            if (rpdbusqueda != null)
            {
                string cosa = null;
                var imagen = rpdbusqueda.Properties["thumbnailPhoto"];

                if (imagen.Count > 0)
                {

                    if (rpdbusqueda.Properties["thumbnailPhoto"][0] is byte[] img)
                    {
                        cosa = $"data:image/png;base64,{Convert.ToBase64String(img)}";
                    }

                }
                usuarios = new UserAd
                {
                    UserName = GetProperty(rpdbusqueda, "cn"),
                    Name = GetProperty(rpdbusqueda, "givenName"),
                    LastName = GetProperty(rpdbusqueda, "sn"),
                    Title = GetProperty(rpdbusqueda, "title"),
                    Department = GetProperty(rpdbusqueda, "department"),
                    Description = GetProperty(rpdbusqueda, "Description"),
                    Company = GetProperty(rpdbusqueda, "company"),
                    City = GetProperty(rpdbusqueda, "l"),
                    State = GetProperty(rpdbusqueda, "st"),
                    Email = GetProperty(rpdbusqueda, "mail"),
                    EmployeeId = Convert.ToInt32(GetProperty(rpdbusqueda, "employeeID")),
                    Foto = cosa,
                    Officce = GetProperty(rpdbusqueda, "physicaldeliveryofficename"),
                    DisplayName = GetProperty(rpdbusqueda, "displayname"),
                    TelephoneNumber = GetProperty(rpdbusqueda, "telephonenumber"),
                    HomePhone = GetProperty(rpdbusqueda, "homePhone"),
                    Mobile = GetProperty(rpdbusqueda, "mobile"),
                    Initials = GetProperty(rpdbusqueda, "Initials"),
                    Manager = GetProperty(rpdbusqueda, "manager"),
                    StreetAddress = GetProperty(rpdbusqueda, "StreetAddress"),
                    PostalCode = GetProperty(rpdbusqueda, "postalCode"),
                    Country = GetProperty(rpdbusqueda, "co"),
                    Sid = GetProperty(rpdbusqueda, "objectSid"),
                    Domain = GetProperty(rpdbusqueda, "Domain")

                };
            }
            else
            {
                usuarios = null;
            }
            return usuarios;
        }

        public List<UserAd> GetListUserActiveDirectory(string ldap)
        {
            UserAd usuarios;
            var path = ldap.Trim();
            DirectoryEntry directory = new DirectoryEntry(path);
            DirectorySearcher mibusqueda = new DirectorySearcher(directory)
            {
                Filter = "(&(objectClass=user)(objectCategory=person)(&(kPMG-User-GOAccountType=2)))"
                //PropertiesToLoad = {"kPMG-User-GOAccountType", "sAMAccountName"}


            };
            var rpdbusqueda = mibusqueda.FindAll();

            // obtiene los resultados de la Busqueda 
            List<UserAd> listuser = new List<UserAd>();

            if (rpdbusqueda.Count > 0)
            {

                foreach (SearchResult item in rpdbusqueda)
                {
                    string cosa = null;
                    var imagen = item.Properties["thumbnailPhoto"];

                    if (imagen.Count > 0)
                    {
                        if (item.Properties["thumbnailPhoto"][0] is byte[] img)
                        {
                            cosa = $"data:image/png;base64,{Convert.ToBase64String(img)}";
                        }

                    }

                    var itemsap = item.Properties["employeeID"];
                    int? codigosap = null;
                    if (itemsap.Count > 0)
                    {

                        var replace = item.Properties["employeeID"][0].ToString();
                        if (replace.Contains("-"))
                        {
                            codigosap = null;
                        }
                        else
                        {
                            codigosap = Convert.ToInt32(replace);
                        }


                    }



                    usuarios = new UserAd
                    {
                        UserName = GetProperty(item, "cn"),
                        Name = GetProperty(item, "givenName"),
                        LastName = GetProperty(item, "sn"),
                        Title = GetProperty(item, "title"),
                        Department = GetProperty(item, "department"),
                        Description = GetProperty(item, "Description"),
                        Company = GetProperty(item, "company"),
                        City = GetProperty(item, "l"),
                        State = GetProperty(item, "st"),
                        Email = GetProperty(item, "mail"),
                        EmployeeId = codigosap,
                        Foto = cosa,
                    };
                    listuser.Add(usuarios);
                }


            }




            //}
            //else
            //{
            //    usuarios = null;
            //}
            return listuser;
        }



        /// <summary>
        /// iniciar seccion en el active directory
        /// </summary>
        /// <param name="userName">nombre de usuario</param>
        /// <param name="pass">contraseña del usuario</param>
        /// <param name="ldap"> LDAP ejemplo: LDAP://ve.kworld.kpmg.com/DC=ve,DC=kworld,DC=kpmg,DC=com </param>
        /// <param name="domainName">dominio ejemplo: ve.kworld.kpmg.com </param>
        /// <returns>retorna true o false</returns>
        public bool LoginActiveDirectory(string userName, string pass, string ldap, string domainName)
        {
            string path = ldap.Trim();
            string dominio = domainName.Trim();

            var usrnamedominio = dominio + @"\" + userName;
            //Creamos el Directorio LDAP
            DirectoryEntry de = new DirectoryEntry(path, usrnamedominio, pass);


            DirectorySearcher buscarUsuario = new DirectorySearcher(de)
            {
                Filter = $"(samaccountname={userName})"
            };
            buscarUsuario.PropertiesToLoad.Add("cn");
            SearchResult result = buscarUsuario.FindOne();
            if (result == null)
            {
                return false;
            }


            return true;


        }


        public enum Campos
        {
            UserName = 1,
            Email = 2,
            EmployeeId = 3,

        }


        private string GetTipocampo(Campos campo)
        {
            string nombreCampo = "UserName";
            switch (campo)
            {
                case Campos.UserName:
                    nombreCampo = "UserName";
                    break;
                case Campos.Email:
                    nombreCampo = "Email";
                    break;
                case Campos.EmployeeId:
                    nombreCampo = "EmployeeId";
                    break;
                default:
                    nombreCampo = "UserName";
                    break;


            }

            return nombreCampo;
        }


        /// <summary>
        /// Consultas por campo predefinidos
        /// </summary>
        /// <param name="consulta">Parametro a enviar en la consulta ejemplo : juanPerez </param>
        /// <param name="tipoCampo">Enumerable de Username, Email, Codigo del empleado</param>
        /// <param name="ldap"></param>
        /// <returns></returns>
        public UserAd GetUserActiveDirector(string consulta, Campos tipoCampo, string ldap)
        {
            string campo = GetTipocampo(tipoCampo);


            UserAd usuarios;
            var path = ldap.Trim();
            DirectoryEntry directory = new DirectoryEntry(path);
            DirectorySearcher mibusqueda = new DirectorySearcher(directory)
            {
                Filter =
                    $"(&((&(objectCategory=Person)(objectClass=User)))({campo}={consulta}))"
            };

            //mibusqueda.PropertiesToLoad.Add("cn"); mibusqueda.SearchScope = SearchScope.Subtree;

            // obtiene los resultados de la Busqueda 
            SearchResult rpdbusqueda = mibusqueda.FindOne();
            if (rpdbusqueda != null)
            {
                string cosa = null;
                var imagen = rpdbusqueda.Properties["thumbnailPhoto"];

                if (imagen.Count > 0)
                {
                    if (rpdbusqueda.Properties["thumbnailPhoto"][0] is byte[] img)
                    {
                        cosa = $"data:image/png;base64,{Convert.ToBase64String(img)}";
                    }

                }

                usuarios = new UserAd
                {
                    UserName = GetProperty(rpdbusqueda, "cn"),
                    Name = GetProperty(rpdbusqueda, "givenName"),
                    LastName = GetProperty(rpdbusqueda, "sn"),
                    Title = GetProperty(rpdbusqueda, "title"),
                    Department = GetProperty(rpdbusqueda, "department"),
                    Description = GetProperty(rpdbusqueda, "Description"),
                    Company = GetProperty(rpdbusqueda, "company"),
                    City = GetProperty(rpdbusqueda, "l"),
                    State = GetProperty(rpdbusqueda, "st"),
                    Email = GetProperty(rpdbusqueda, "mail"),
                    EmployeeId = Convert.ToInt32(GetProperty(rpdbusqueda, "employeeID")),
                    Foto = cosa,
                    Officce = GetProperty(rpdbusqueda, "physicaldeliveryofficename"),
                    DisplayName = GetProperty(rpdbusqueda, "displayname"),
                    TelephoneNumber = GetProperty(rpdbusqueda, "telephonenumber"),
                    HomePhone = GetProperty(rpdbusqueda, "homePhone"),
                    Mobile = GetProperty(rpdbusqueda, "mobile"),
                    Initials = GetProperty(rpdbusqueda, "Initials"),
                    Manager = GetProperty(rpdbusqueda, "manager"),
                    StreetAddress = GetProperty(rpdbusqueda, "StreetAddress"),
                    PostalCode = GetProperty(rpdbusqueda, "postalCode"),
                    Country = GetProperty(rpdbusqueda, "co"),
                    Sid = GetProperty(rpdbusqueda, "objectSid"),
                    Domain = GetProperty(rpdbusqueda, "Domain")
                };

            }
            else
            {
                usuarios = null;
            }
            return usuarios;
        }


    }
}
