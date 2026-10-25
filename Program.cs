using System;
using System.IO;

string carpetaFicheros = "./CarpetaFicheros";
string rutaArchivo = Path.Combine(carpetaFicheros, "archivo.txt");
string opcion = "";
string mensaje = "";

if (!Directory.Exists(carpetaFicheros))
{
    Directory.CreateDirectory(carpetaFicheros);
}

while(opcion != "6")
{
    Console.Clear();
    
    Console.WriteLine("--- MENÚ ---");
    Console.WriteLine("1. Crea un nuevo fichero.");
    Console.WriteLine("2. Añadir registros en el fichero.");
    Console.WriteLine("3. Eliminar todos los registros del fichero.");
    Console.WriteLine("4. Eliminar el fichero.");
    Console.WriteLine("5. Visualizar los registros.");
    Console.WriteLine("6. Cerrar la aplicación.");

    
    if (!string.IsNullOrEmpty(mensaje))
    {
        Console.WriteLine("\n" + mensaje);
    }

    Console.Write("\nSelecciona una opción: ");
    opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Console.Clear();
            Console.Write("Introduce el nombre del fichero (si no se escribe nada, se crea por defecto archivo.txt): ");
            string nombre = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nombre))
            {
                rutaArchivo = Path.Combine(carpetaFicheros, nombre);
            }
            
            try {
                FileStream archivo = File.Create(rutaArchivo);
                archivo.Close();
                mensaje = $"¡El archivo [{Path.GetFileName(rutaArchivo)}] se ha creado exitosamente!";
            }
            catch(Exception ex)
            {
                mensaje = "Error al crear el archivo" + ex.Message;
            }
            break;

        case "2":
            if (Directory.GetFiles(carpetaFicheros).Length == 0)
            {
                mensaje = "No hay ningún fichero creado. Selecciona la opción 1 para crear uno.";
            }
            else
            {
                rutaArchivo = SeleccionarFichero(carpetaFicheros, "donde quieres añadir el registro: ");

                if (File.Exists(rutaArchivo))
                {
                    Console.Write("Introduce el registro que quieres añadir: ");
                    string nuevoRegistro = Console.ReadLine();

                    List<string> lineas = File.ReadAllLines(rutaArchivo).ToList();
                    lineas.Add(nuevoRegistro);
                    File.WriteAllLines(rutaArchivo, lineas);

                    mensaje = $"¡Registro añadido exitosamente en [{Path.GetFileName(rutaArchivo)}]!";
                }
                else
                {
                    mensaje = "El archivo especificado no existe.";
                }
            }
            break;

        case "3":
            if (Directory.GetFiles(carpetaFicheros).Length == 0)
            {
                mensaje = "No hay ningún fichero creado. Selecciona la opción 1 para crear uno.";
            }
            else
            {
                rutaArchivo = SeleccionarFichero(carpetaFicheros, "donde quieres eliminar todos los registros: ");
                try
                {
                    if (File.Exists(rutaArchivo))
                    {
                        File.WriteAllText(rutaArchivo, string.Empty);
                        mensaje = $"¡Todos los registros de {Path.GetFileName(rutaArchivo)} han sido eliminados correctamente!";
                    }
                    else
                    {
                        mensaje = "El archivo no existe.";
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "Error al vaciar el archivo: " + ex.Message;
                }
                }
            break;

        case "4":
            if (Directory.GetFiles(carpetaFicheros).Length == 0)
            {
                mensaje = "No hay ningún fichero creado. Selecciona la opción 1 para crear uno.";
            }
            else
            {
                rutaArchivo = SeleccionarFichero(carpetaFicheros, "que quieres eliminar: ");

                try
                {
                    if (File.Exists(rutaArchivo))
                    {
                        File.Delete(rutaArchivo);
                        mensaje = $"¡El fichero [{Path.GetFileName(rutaArchivo)}] ha sido eliminado exitosamente!";
                    }
                    else
                    {
                        mensaje = "El archivo no existe.";
                    }
                }
                catch (Exception ex)
                {
                    mensaje = "Error al eliminar el archivo: " + ex.Message;
                }
            }
            break;

        case "5":
            if (Directory.GetFiles(carpetaFicheros).Length == 0)
            {
                mensaje = "No hay ningún fichero creado. Selecciona la opción 1 para crear uno.";
            }
            else
            {
                rutaArchivo = SeleccionarFichero(carpetaFicheros, "que quieres visualizar: ");
            
                if (File.Exists(rutaArchivo))
                {
                    string[] lineasLectura = File.ReadAllLines(rutaArchivo).ToArray();
                    Console.WriteLine($"\nContenido de {Path.GetFileName(rutaArchivo)}: ");
                    if (lineasLectura.Length == 0)
                    {
                        Console.WriteLine("El archivo está vacío");
                    }
                    else
                    {
                        foreach (string s in lineasLectura)
                        {
                            Console.WriteLine(s);
                        }
                    }
                    mensaje = "";
                    Console.WriteLine("\nPresiona ENTER para volver al menú.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("El archivo no existe. Créalo primero con la opción 1.");
                }
            }
            break;

        case "6":
            Console.WriteLine("Saliendo de la aplicación...");
            break;

        default:
            mensaje = "Opción no válida. Inténtalo de nuevo.";
            break;
    }
}

void MostrarFicherosDisponibles(string carpeta)
{
    Console.WriteLine("Ficheros disponibles:");
    string[] archivos = Directory.GetFiles(carpeta);

    foreach (string archivo in archivos)
    {
        Console.WriteLine("- " + Path.GetFileName(archivo));
    }
}

string SeleccionarFichero(string carpeta, string accion)
{
    Console.Clear();
    MostrarFicherosDisponibles(carpeta);

    Console.Write($"\nIntroduce el nombre del fichero {accion}");
    string nombre = Console.ReadLine();

    return Path.Combine(carpeta, nombre);
}