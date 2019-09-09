Notes from the developed solution:
----------------------------------

## The solution constitutes of 4 components which run on startin the project which need to interact with each other.
Consumer - Consuming application
Publishers - PublisherOne and PublisherTwo 
Subscriber
## Kindly set up multiple start up projects prior to running the applications.
## The .Net framework in which the zipped solution was provided in was using version 4.5. This version does not allow
using any of the service buses (Azure Service Bus or NServiceBus).Hence I have upgraded the .Net Framework application from 4.5 to 4.7.1. This will enable me to use the
different available Service Buses.
## Assumptions for the solution:
  1. Subscriber registration cannot occur prior to Publisher registration since no channel is available to subscribe to.
  2. Publishers are nominating a string ChannlName - PublisherOne=ChannelOne and PublisherTwo=ChannelTwo.


Message Bus Fun
===============

Your challenge – should you choose to accept it – is to design a component according to the
following criteria.

Non-functional acceptance criteria.
-----------------------------------

### Overview

Assume – in a corporate context - that you are creating a small solution to demonstrate your
design philosophy and essential concepts to a trainee. The solution should therefore contain an
example illustrating each fundamental concept or mechanism that you consider to be important.

### Submission format.

Any Visual Studio solution version between 2013 and 2017 is permissible. As far as possible, the
solution should be useable with no special preparation of the development environment. Any
instructions or usage notes should be in added to the beginning  of this readme. Running tests from
the command-line is acceptable.

### Language, framework, and dependencies.
The solution should be authored in C# targeting the .Net Framework version 4.0 or above.
All non-core references should be obtainable via NuGet.

### Target Operating Environment

A library. Assume that the component can be shared in binary form with multiple other development
teams for incorporation into various applications. The component is intended to be utilised
directly in-process; it is not necessary to manage the exposure of any functionality or state via
any mechanism other than by the use of visibility modifiers. The applications that consume
the component will be running in either a server or client context.

Functional description.
-----------------------

### Library

* The component is intended to manage the routing of messages from multiple providers -
  grouped into channels - to multiple subscribers. The reliability of the routing is key. Any API or
  interface necessary to the routing mechanism are the domain of the component.
* Consuming applications should be able to register and deregister both subscribers and
  providers.
* Providers will nominate a channel name (a string) upon registration. Multiple providers
  may nominate the same channel name. A provider cannot change channel names without
  deregistering.
* The consuming application should be able to read a list of available channels. An available
  channel is one for which at least one provider is currently registered.
* Consuming applications should be able to register a subscriber to receive messages from any
  available channel/s without limitation.
* Once registered to an available channel, the subscriber will receive all messages sent by
  registered providers on that channel.
* The subscriber needs to identify which channel a message originates from, but not the identity
  of the provider.
* Any subscribers are notified if a channel becomes unavailable, but are not automatically
  unsubscribed. If the channel becomes available again, subscribers still registered will once again
  receive messages from it.
* Multiple attempts to register a subscriber to receive messages to the same channel should have
  no effect.
* Multiple attempts to deregister a subscriber from receiving messages from the same channel
  should have no effect.
* Attempts to deregister a subscriber which is not registered should have no effect.
* A consuming application should be able to deregister a subscriber from receiving messages from
  any channels without limitation.
* A consuming application should be able to deregister a provider from publishing messages.
* Multiple attempts to deregister the same provider from publishing messages should have no
  effect.
* Attempts to deregister a provider which is not registered should have no effect.
