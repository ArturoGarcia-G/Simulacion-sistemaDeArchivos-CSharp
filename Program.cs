
class carpeta
{
    public string[] archivos = new string[1];

    public carpeta[] hijos = new carpeta[1];
    public carpeta padre;
    public string nombre;
    public string propietario;

    public carpeta()
    {
        archivos[0] = "";
        hijos[0] = null;
        padre = null;
        nombre = "";
        propietario = "";
    }
}

class Arbol
{
    private carpeta raiz;

    public Arbol()
    {
        raiz = new carpeta();
    }

    public carpeta Insertar(string nombre, carpeta padre, string propietario)
    {
        if(padre == null )
        {
            carpeta raiz = new carpeta();
            raiz.nombre = nombre;
            raiz.padre = null;
            raiz.hijos[0] = null;
            raiz.archivos[0] ="";
            raiz.propietario="";
            return raiz;
        }
        else
        {

            carpeta temp = new carpeta();
            temp.nombre = nombre;
            temp.padre = padre;
            padre.hijos[padre.hijos.Length - 1] = temp;
            Array.Resize(ref padre.hijos, padre.hijos.Length + 1);
            temp.archivos[0] = "";
            temp.propietario = propietario;
            return temp;
        }

        
    }
}

class Program
{
    static void Main (string[] args)
    {
        int mostrarHijos (carpeta directorio)
        {
            if(directorio== null)
            {
                return 0;
            }
            int numeroCarpetas = 0;
            Console.WriteLine("Carpetas: ");
            for (int i = 0; i < directorio.hijos.Length; i++)
            {
                if(directorio.hijos[i] != null)
                {
                    Console.WriteLine((i+1) + ". " + directorio.hijos[i].nombre);
                    numeroCarpetas++;
                }
                
            }

            return numeroCarpetas;
        }

        int mostrarArchivos(carpeta directorio)
        {
            if (directorio == null)
            {
                return 0;
            }
            int numeroArchivos = 0;
            Console.WriteLine("Archivos: ");
            for (int i = 0; i < directorio.archivos.Length; i++)
            {
                if (directorio.archivos[i] != "")
                {
                    Console.WriteLine((i + 1)+ ". " + directorio.archivos[i]);
                    numeroArchivos++;
                }

            }

            return numeroArchivos;
        }
        string direccion="";
        string origen (carpeta actual, carpeta padre)
        {
            if(actual.padre==null)
            {
                return direccion;
            }
            else
            {
                direccion = "/" + padre.nombre + direccion;
                direccion = origen(padre, padre.padre);
                return direccion;
            }
            

        }

        int opc=0;
        Arbol arbol = new Arbol();
        carpeta raiz = arbol.Insertar("C:", null,"");
        carpeta carpetaActual = raiz;

        string[] usuarios = new string[2];
        string[] contraseñas = new string[2];

        usuarios[0] = "usuario1";
        usuarios[1] = "usuario2";
        contraseñas[0] = "contraseña1";
        contraseñas[1] = "contraseña2";



        int ciclo = 0;
        do
        {
            Console.WriteLine("Usuario: ");
            string usuario = Console.ReadLine();
            Console.WriteLine("Contraseña: ");
            string contraseña = Console.ReadLine();
            int acceso = 0;

            for (int i = 0; i < 2; i++)
            {
                if (usuario.Equals(usuarios[i]) == true && contraseña.Equals(contraseñas[i]) == true)
                {
                    acceso = 1;
                    do
                    {
                        string carpe = carpetaActual.nombre;
                        // if (carpe.Equals("C:"))
                        //{
                        //  carpe = "";
                        // }
                        direccion = "";
                        Console.ForegroundColor= ConsoleColor.Red;
                        Console.WriteLine(direccion=origen(carpetaActual, carpetaActual.padre)+ "/"+ carpe);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("");
                        Console.WriteLine("------Menu---------");
                        Console.WriteLine("1. Crear carpeta");
                        Console.WriteLine("2. Acceder a carpeta");
                        Console.WriteLine("3. Eliminar carpeta");
                        Console.WriteLine("4. Crear Archivo");
                        Console.WriteLine("5. Eliminar Archivo");
                        Console.WriteLine("6. Abrir Archivo");
                        Console.WriteLine("7. Navegar");
                        Console.WriteLine("8. Salir");
                        Console.WriteLine("-------------------");
                        opc = int.Parse(Console.ReadLine());

                        if (opc == 1)
                        {
                            // CREAR CARPETA
                            if ( carpetaActual.propietario.Equals(usuario) == true || carpetaActual.propietario=="")
                            {
                                Console.WriteLine("Nombre de la carpeta");
                                string nombre = Console.ReadLine();

                                carpeta nuevaCarpeta = arbol.Insertar(nombre, carpetaActual, usuario);
                            }
                            else
                            {
                                Console.WriteLine("No tiene los permisos necesarios");
                            }
                            

                        }
                        else if (opc == 2)
                        {
                            // ACCEDER A CARPETA 
                            int carpetas = 0, acceder = 0;
                            carpetas = mostrarHijos(carpetaActual);
                            if (carpetas != 0)
                            {
                                Console.WriteLine("Seleccione la carpeta a acceder");
                                acceder = int.Parse(Console.ReadLine());
                                if (acceder <= carpetas && carpetaActual.hijos[acceder-1].propietario == usuario)
                                {
                                    carpetaActual = carpetaActual.hijos[acceder - 1];
                                }
                                else
                                {
                                    Console.WriteLine("No tiene los permisos necesarios para acceder");
                                }

                            }
                            else
                            {
                                Console.WriteLine("No hay carpetas disponibles en esta ruta");

                            }


                        }
                        else if (opc == 3)
                        {
                            //ELIMINAR CARPETA
                            int carpetas = 0, eliminar = 0;
                            carpetas = mostrarHijos(carpetaActual);
                            if (carpetas != 0)
                            {
                                Console.WriteLine("Seleccione la carpeta a eliminar");
                                eliminar = int.Parse(Console.ReadLine());
                                if (eliminar <= carpetas && carpetaActual.hijos[eliminar - 1].propietario == usuario)
                                {
                                    carpetaActual.hijos[eliminar - 1] = null;
                                }
                                else
                                {
                                    Console.WriteLine("No tiene los permisos necesarios para eliminar ");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay carpetas disponibles en esta ruta");
                            }
                        }
                        else if (opc == 4)
                        {
                            // CREAR ARCHIVO
                            Console.WriteLine("Nombre del archivo");
                            string nombre = Console.ReadLine();

                            carpetaActual.archivos[carpetaActual.archivos.Length - 1] = nombre;
                            Array.Resize(ref carpetaActual.archivos, carpetaActual.archivos.Length + 1);

                        }
                        else if (opc == 5)
                        {
                            int carpetas = 0, eliminar = 0;
                            carpetas = mostrarArchivos(carpetaActual);
                            if (carpetas != 0)
                            {
                                Console.WriteLine("Seleccione la carpeta a eliminar");
                                eliminar = int.Parse(Console.ReadLine());
                                if (eliminar <= carpetas)
                                {
                                    carpetaActual.archivos[eliminar - 1] = "";
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay archivos disponibles en esta ruta");
                            }

                        }
                        else if (opc == 6)
                        {
                            int carpetas = 0, abrir = 0;
                            carpetas = mostrarArchivos(carpetaActual);
                            if (carpetas != 0)
                            {
                                Console.WriteLine("Seleccione el archivo a abrir");
                                abrir = int.Parse(Console.ReadLine());
                                if (abrir <= carpetas)
                                {
                                    Console.WriteLine("---------------");
                                    Console.WriteLine("|    {0}     |", carpetaActual.archivos[abrir - 1]);
                                    Console.WriteLine("|              |");
                                    Console.WriteLine("| XXXXXXXXXX   |");
                                    Console.WriteLine("| XXXXXXXXXX   |");
                                    Console.WriteLine("| XXXXXXXXXX   |");
                                    Console.WriteLine("| XXXXXXXXXX   |");
                                    Console.WriteLine("| XXXXXXXXXX   |");
                                    Console.WriteLine("| XXXXXXXXXX   |");
                                    Console.WriteLine("---------------");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No hay archivos disponibles en esta ruta");
                            }

                        }
                        else if (opc == 7)
                        {
                            int opcion = 0;
                            Console.WriteLine("------Menu---------");
                            Console.WriteLine("1. Subir nivel");
                            Console.WriteLine("2. Subir a la raiz");
                            Console.WriteLine("3. Path");
                            Console.WriteLine("-------------------");
                            opcion = int.Parse(Console.ReadLine());
                            if (opcion == 1)
                            {
                                if (carpetaActual.padre != null)
                                {
                                    carpetaActual = carpetaActual.padre;
                                }
                                else
                                {
                                    Console.WriteLine("No se puede subir nivel, Ya esta en la raiz");
                                }

                            }
                            else if (opcion == 2)
                            {
                                carpetaActual = raiz;
                            }
                            else if (opcion == 3)
                            {
                                Console.WriteLine("Path:");
                                string path = Console.ReadLine();
                                string[] words = path.Split('/');
                                for(int j = 0; j < words.Length; j++)
                                {
                                    if(words[j] =="C:")
                                    {
                                        carpetaActual = raiz;
                                    }
                                    else
                                    {
                                        for(int k = 0;k < carpetaActual.hijos.Length; k++)
                                        {
                                            if(carpetaActual.hijos[k]!=null)
                                            {
                                                if (carpetaActual.hijos[k].nombre.Equals(words[j]) == true)
                                                {
                                                    carpetaActual = carpetaActual.hijos[k];
                                                }
                                            }
                                            
                                        }
                                    }
                                }

                            }


                        }
                        //else if (opc==8)
                        //{
                        //    carpeta directorio = raiz;
                        //    int k=0;
                        //    int j=0;
                        //    while(directorio.hijos[0]!=null)
                        //    {
                        //        while(directorio.hijos[k]!=null)
                        //        {
                        //            Console.Write(directorio.hijos[k].nombre + "  ");
                        //            k++;
                        //        }
                        //        j++;
                        //        directorio = directorio.hijos[j];
                        //    }
                       // }


                    } while (opc != 8);

                }


            }
            if (acceso == 0)
            {
                Console.WriteLine("Usuario o contraseña incorrectos");
               
            }
            usuario = "";
            contraseña = "";

        } while (ciclo == 0);

        



        
    }
}