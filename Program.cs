using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace voiting_uygulamasi
{
    class Program
    {
        static List<User> users = new List<User>();
        static List<Category> categories;
        static User currentUser;
        static void Main(string[] args)
        {
            categories = new List<Category>
                {
                    new Category { categoryID = 1, categoryName = "Film Kategorileri" },
                    new Category { categoryID = 2, categoryName = "Tech Stack Kategorileri" },
                    new Category { categoryID = 3, categoryName = "Spor Kategorileri" }
                };

            do
            {
                Console.WriteLine("Kullanıcı adınızı giriniz: ");
                string userName = Console.ReadLine();

                currentUser = users.FirstOrDefault(u => u.UserName == userName);

                if (currentUser == null)
                {
                    Console.WriteLine("Uygulamaya kaydınız bulunmamaktadır. Girmiş olduğunuz kullanıcı adı ile kaydınıza devam etmek istiyor musunuz?");
                    Console.WriteLine("E/H olarak seçim yapınız!");
                    bool section;
                    do
                    {
                        switch (Console.ReadLine().ToUpper())
                        {
                            case "E":
                                section = false;
                                break;

                            case "H":
                                section = false;
                                Console.WriteLine("Lütfen kayıt için kullanıcı adınızı giriniz!");
                                userName = Console.ReadLine();
                                break;

                            default:
                                Console.WriteLine("Lütfen Evet ise E, Hayır ise H yazınız!");
                                section = true;
                                break;
                        }
                    } while (section);


                    currentUser = new User { UserName = userName };
                    users.Add(currentUser);
                }

                if (currentUser.VotedCategoryIndex == 0)
                {
                    UseVote();
                    Console.WriteLine("Yeni bir kullanıcı adı ile oy vermeye devam edebilirsiniz?");
                    Console.WriteLine("E/H");
                    if (Console.ReadLine() == "h")
                    {
                        break;
                    }
                }

            } while (true);
        }

        public static void UseVote()
        {
            Console.WriteLine("Aşağıdaki kategorilerden birine oy verin!");
            for (int i = 0; i < categories.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {categories[i].categoryName}");
            }

            int selectedCategoryIndex;
            bool isSelected = int.TryParse(Console.ReadLine(), out selectedCategoryIndex);

            while (true)
            {
                if (!isSelected || selectedCategoryIndex > 3)
                {
                    Console.WriteLine("\nGeçersiz kategori numarası. Lütfen tekrar deneyin.\n");
                }
                else if (selectedCategoryIndex >= 1 && selectedCategoryIndex <= categories.Count)
                {
                    // Kategoriye oy verme
                    currentUser.VotedCategoryIndex = selectedCategoryIndex - 1;
                    Console.WriteLine($"Teşekkür ederiz! {categories[selectedCategoryIndex - 1].categoryName} kategorisine oy verdiniz.");
                    break;
                }
            }

            Console.WriteLine("\nOylama Sonuçları:");
            for (int i = 0; i < categories.Count; i++)
            {
                int voteCount = users.Count(u => u.VotedCategoryIndex == i);
                double votePercentage = (double)voteCount / users.Count * 100;
                Console.WriteLine($"{categories[i].categoryName}: {voteCount} oy, %{votePercentage:F2}");
            }
        }
    }
}
