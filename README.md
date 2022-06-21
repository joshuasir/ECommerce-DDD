# E-Commerce DDD Pattern Design

The purpose of this project is to use the DDD approach in trying to solve an E-commerce system infrastructure. This documentation describes the problem domain in the form of text explanations, UML diagrams, and implementation of concepts in coding. The implementation of coding using .NET Core technology, has not been integrated to an application.

__E-commerce subdomain distilation__
- Core subdomain : Product Catalog, Orders
- Supporting subdomain : Inventory System, Shipping
- Generic subdomain : Invoicing, External Forecasting System

![image](https://user-images.githubusercontent.com/71873035/172210235-b2c71c6c-ac3d-4832-b1f8-c0f5813aceff.png)

_Reference: Implementing Domain-Driven Design, Vaughn Vernon_

__Scenario__

_Product Catalogue_
- Browse catalogue/collection
- Browse category
- Update product info
- Update category info
- Add to cart

_Orders_
- Order Product
- Fill order form
- Choose Shipping method
- Choose Payment method

_Inventory System_
- Add/restock product
- Fetch product for delivery

_Shipping_
- Dispatch a delivery

_Invoicing_
- Invoice a billing

_External Forecasting System_
- Decide stock quantity need


__Ubiquitous Language__
- Browse: survey goods/products
- Orders: stated intention to purchase a certain list of product(s).
- Cart: container that hold list of user’s selected item to buy.
- Add to Cart: add product to customer cart without doing transaction.
- Checkout: a transaction process to buy product.
- Form: a document to filled by customer.
- Product Catalogue: List of product(s).
- Product Category: a name of group of product(s).
- Shipping/delivery: a method to send purchased product(s) to customer
- Payment: action of paying a bill/invoice.
- Invoicing: an invoice, bill or tab is a commercial document issued by a seller to a buyer,
- relating to a sale transaction and indicating the products, quantities, and agreed upon prices for
- products or services the seller had provided the buyer.(https://en.wikipedia.org/wiki/Invoice)
- Stock: Amount of available quantity.
- Restock: Readd the amount of available quantity..
- Price: the amount of money expected, required, or given in payment for something.
- Product Variant, Option: A various type of product that can be choose.


Core subdomain : 
  - Product Catalog
    - Product Detailing Bounded Context
  - Orders:
    - Sales Bounded Context
    - Billing Bounded Context
    - Shipping Bounded Context

Supporting subdomain :  
  - Inventory System
  - Shipping

Generic subdomain :
  - Invoicing
  - External Forecasting System


The following are the implementation of Core subdomain with .NET technology


![UML](https://user-images.githubusercontent.com/71873035/172282963-5d273039-c444-410b-8ed7-ac7bb665d0e0.png)

__Entity and Value Objects__
Value Object as seen in the diagram inherits ValueObject class while Entity inherits EventSourcedAggregate which inherits Entity<TId> class

__Domain Events__
- OrderPlacedEvent, order ordering event on the Order domain
- CartCheckoutEvent, cart checkout event on the Cart domain
- FillCustomerFormEvent, event filling customer data form in Customer domain
  
 _File: in the Events folder of each Domain_

there are also 2 additional event domains for the payment domain
- PaymentCreatedEvent, payment slip issuance event
- PaymentAuthorizedEvent, payment validation event

__Domain Services__
- ShippingCostCalculator - calculates the cost of shipping domain orders
- MailInvoicer - send invoice for domain payment
- NewsLetter - send newsletter to customer domain sales
  
  _File: in the Services folder of each Domain_

__5 Aggregate Root describing the domain concepts of the problem into smaller parts, that is__
- Aggregate Order, which is the aggregate root domain problem Order, holds the aggregate root Customer and Cart references
- Aggregate Payment, is the aggregate root domain problem Payment, holds the reference to the aggregate root Customer and Order
- Aggregate Customer, is the aggregate root domain problem Customer
- Aggregate Product, is the aggregate root domain problem Product/Sales
- Aggregate Cart, an aggregate root domain problem Cart/Sales

_File : Infrastructure/AggregateRoot, Infrastructure/EventSourcedAggregate, aggregate on each Domain_

  
__Event Sourcing__
Each of these aggregates applies the concept of event sourcing so that they have a list of DomainEvents, the nature of the event sourcing implemented by this aggregate is named EventSourcedAggregate. Then for the event storage itself, we use the purpose-built event store package provided by .net core.

  _File : Infrastructure/EventStored/*, Infrastructure/EventSourcedAggregate_

  
__Factories__
For each entity we also create a Factory in the form of a CreateNew method which handles the complex logic of creating a new entity, a more complete example is in the coding.

  _File: the model for each domain in the Domains folder has a CreateNew method_

  
__Repositories__
In addition to implementing Event Sourcing, we also implement a database, the repository here acts as a liaison between our storage and applications, both storage event stores and dbcontext.

  _File : Infrastructure/Repository/*_

  
Due to time and source limitation, there are still implementation to be made such as implementing CQRS pattern, integrating with WebAPI and frontEnd Web Application.



references : 
- https://sd.blackball.lv/library/patterns_principles_and_practices_of_domain-driven_design_(2015).pdf
- https://github.com/falberthen/EcommerceDDD
