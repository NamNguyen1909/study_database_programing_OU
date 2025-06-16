using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        StudentManager manager = new StudentManager();
    //        while (true)
    //        {
    //            Console.WriteLine("\nChon chuc nang:");
    //            Console.WriteLine("1. Hien thi danh sach sinh vien");
    //            Console.WriteLine("2. Them sinh vien");
    //            Console.WriteLine("3. Sua thong tin sinh vien");
    //            Console.WriteLine("4. Xoa sinh vien");
    //            Console.WriteLine("5. Thoat");
    //            Console.Write("Nhap lua chon: ");
    //            string choice = Console.ReadLine();

    //            switch (choice)
    //            {
    //                case "1":
    //                    manager.DisplayStudents();
    //                    break;
    //                case "2":
    //                    Console.Write("Nhap ho ten: ");
    //                    string fullName = Console.ReadLine();
    //                    Console.Write("Nhap tuoi: ");
    //                    int age = int.Parse(Console.ReadLine());
    //                    Console.Write("Nhap chuyen nganh: ");
    //                    string major = Console.ReadLine();
    //                    manager.AddStudent(fullName, age, major);
    //                    break;
    //                case "3":
    //                    Console.Write("Nhap ID sinh vien can sua: ");
    //                    int updateID = int.Parse(Console.ReadLine());
    //                    Console.Write("Nhap ho ten moi: ");
    //                    string newFullName = Console.ReadLine();
    //                    Console.Write("Nhap tuoi moi: ");
    //                    int newAge = int.Parse(Console.ReadLine());
    //                    Console.Write("Nhap chuyen nganh moi: ");
    //                    string newMajor = Console.ReadLine();
    //                    manager.UpdateStudent(updateID, newFullName, newAge, newMajor);
    //                    break;
    //                case "4":
    //                    Console.Write("Nhap ID sinh vien can xoa: ");
    //                    int deleteID = int.Parse(Console.ReadLine());
    //                    manager.DeleteStudent(deleteID);
    //                    break;
    //                case "5":
    //                    Console.WriteLine("Tam biet!");
    //                    return;
    //                default:
    //                    Console.WriteLine("Lua chon khong hop le!");
    //                    break;
    //            }
    //        }
    //    }
    //}

    // Program entry point
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
