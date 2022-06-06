
#Domain Events
File : pada folder Events masing masing Domain
OrderPlacedEvent - event pemesanan order pada domain Order
CartCheckoutEvent - event checkout cart pada domain Cart
FillCustomerFormEvent - event pengisian form data customer pada domain Customer

terdapat juga 2 domain event tambahan untuk domain payment
PaymentCreatedEvent - event pengeluaran slip payment
PaymentAuthorizedEvent - event validasi pembayaran

#Domain Services
File : pada folder Services masing masing Domain
ShippingCostCalculator - menghitung biaya pengiriman domain order
MailInvoicer - mengirim invoice pembayaran domain payment
NewsLetter - mengirim newsletter ke customer domain sales

#5 Aggregate Root yang menggambarkan domain concepts dari problem 
menjadi bagian yang lebih kecil, yaitu
- Aggregate Order, merupakan aggregate root domain problem Order, memegang referensi aggregate root Customer dan Cart
- Aggregate Payment, merupakan aggregate root domain problem Payment, memegang referensi aggregate root Customer dan Order
- Aggregate Customer, merupakan aggregate root domain problem Customer
- Aggregate Product, merupakan aggregate root domain problem Product/Sales
- Aggregate Cart, merupakan aggregate root domain problem Cart/Sales
File : Infrastructure/AggregateRoot, Infrastructure/EventSourcedAggregate, aggregate pada masing masing Domain

#Event Sourcing
Masing masing dari aggregate ini menerapkan konsep event sourcing sehingga memiliki list of DomainEvent, sifat event sourcing yang diterapkan oleh aggragate ini diberi nama EventSourcedAggregate. Lalu untuk event storage itu sendiri kami menggunakan package purpose‐built event store yang disediakan .net core. 
File : Infrastructure/EventStored/*, Infrastructure/EventSourcedAggregate

#Factories
Untuk masing masing entitas kami juga membuat Factory berupa method CreateNew yang menangani complex logic dari pembuatan entitas baru, contoh lebih lengkapnya terdapat pada codingan.
File : model pada masing masing domain dalam folder Domain memiliki method CreateNew

#Repositories
Selain menerapkan Event Sourcing kami juga mengimplementasikan database, repository disini berperan sebagai penghubung antar storage dan applikasi kami, baik storage event store maupun dbcontext.
File : Infrastructure/Repository/*

