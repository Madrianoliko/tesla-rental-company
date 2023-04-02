API dla firmy wypożyczającej samochody tesli. 

API udostępnia 5 kontrolerów: 
- Samochód
- Salon samochodowy
- Rezerwacja
- Użytkownik
- Uwierzytelnienie

W kontrolerach zastosowana jest wymagana autoryzacja dostępu do zasobu, a dzięki użyciu polityki bezpieczeństwa, do niektórych kontrolerów ma dostęp tylko admin.

![image](https://user-images.githubusercontent.com/51478114/229370792-58f3c823-fdb8-466b-9c57-fe7ca55db327.png)

Aplikacja oparta jest o przedstawiony wyżej model danych, dlatego każde utworzenie nowej rezerwacji, przypisze ją do użytkownika i samochodu.

Mechanizm API został zaimplementowany z wykorzystaniem Repository Pattern, gdzie przetrzymywane są główne metody działania na bazie danych, do których odwołują się kontrolery.

Kontrolery nie działają bezpośrednio na obiektach bazy danych tylko korzystają z modeli Dto. Do mapowania modeli użyto biblioteki AutoMapper. Profile mapowania mieszczą się w folderze Profiles.

Jako ORM użyto Entity Framework Core, a jego kontekst zapisany jest w pliku TeslaRentalCompanyContext.cs, gdzie znajdują się również początkowe dane dla aplikacji.
