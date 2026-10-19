namespace Phone.Book
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var repository = new ContactRepository();
            var mailSender = new MailSender();

            while (true)
            {
                Console.WriteLine("\n1. View contacts");
                Console.WriteLine("2. Add contact");
                Console.WriteLine("3. Update contact");
                Console.WriteLine("4. Delete contact");
                Console.WriteLine("5. Send email");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                var option = Console.ReadLine();
                if (option == "0" || option == null)
                {
                    return;
                }

                try
                {
                    switch (option)
                    {
                        case "1":
                            var contacts = repository.GetAllContacts();
                            foreach (var contact in contacts)
                            {
                                Console.WriteLine($"{contact.Id}. {contact.Name} | {contact.PhoneNumber} | {contact.Email} | {contact.Group}");
                            }
                            break;
                        case "2":
                            await repository.AddContact(ReadContact());
                            Console.WriteLine("Contact added.");
                            break;
                        case "3":
                            Console.Write("Contact id: ");
                            if (!int.TryParse(Console.ReadLine(), out var updateId))
                            {
                                Console.WriteLine("Invalid id.");
                                break;
                            }
                            await repository.UpdateContact(updateId, ReadContact());
                            Console.WriteLine("Contact updated.");
                            break;
                        case "4":
                            Console.Write("Contact id: ");
                            if (!int.TryParse(Console.ReadLine(), out var deleteId))
                            {
                                Console.WriteLine("Invalid id.");
                                break;
                            }
                            await repository.DeleteContact(deleteId);
                            Console.WriteLine("Contact deleted.");
                            break;
                        case "5":
                            Console.Write("Contact id: ");
                            if (!int.TryParse(Console.ReadLine(), out var recipientId))
                            {
                                Console.WriteLine("Invalid id.");
                                break;
                            }
                            var recipient = await repository.GetContact(recipientId);
                            if (recipient == null)
                            {
                                Console.WriteLine("Contact not found.");
                                break;
                            }
                            Console.Write("Subject: ");
                            var subject = Console.ReadLine() ?? "";
                            Console.Write("Message: ");
                            var body = Console.ReadLine() ?? "";
                            try
                            {
                                await mailSender.Send(recipient.Email, recipient.Name, subject, body);
                                Console.WriteLine("Email sent.");
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine($"Could not send email: {exception.Message}");
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (InvalidOperationException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                {
                    Console.WriteLine("Could not save contact. Check that phone number and email are unique.");
                }
            }
        }

        static ContactEntity ReadContact()
        {
            Console.Write("Name: ");
            var name = Console.ReadLine();
            string phoneNumber;
            while (true)
            {
                Console.Write("Phone number: ");
                phoneNumber = Console.ReadLine() ?? throw new InvalidOperationException("Input ended.");
                if (System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^\+[1-9][0-9]{9,14}\z"))
                {
                    break;
                }
                Console.WriteLine("Invalid phone number. Enter + followed by 10 to 15 digits.");
            }

            string email;
            while (true)
            {
                Console.Write("Email: ");
                email = Console.ReadLine() ?? throw new InvalidOperationException("Input ended.");
                if (System.Net.Mail.MailAddress.TryCreate(email, out var address) && address.Address == email)
                {
                    break;
                }
                Console.WriteLine("Invalid email.");
            }

            Console.Write("Group (Family, Friends, Work, etc.): ");
            var group = Console.ReadLine();

            return new ContactEntity
            {
                Name = name ?? "",
                PhoneNumber = phoneNumber,
                Email = email,
                Group = group ?? ""
            };
        }
    }
}
