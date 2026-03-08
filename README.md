# IGD-Final-Project-2026

\*\*UPDATE 2\*\*



While we initially had a much more ambitious idea for this project in mind, we realized that our concept was not manageable with just two of us and the many other classes/outside responsibilities we had. As such, we made the decision to readjust our project's aim and instead go for something that fits better with our own schedules. With this in mind, we went for a clicker game instead.







As of now, we only have the bare minimum: A manual button that adds one log to the player's resources, and two upgrades that adds passive resources. One (the lumberjack) adds 1 log per second per upgrade. The other (the sawmill) adds 1 plank per second per upgrade. This implementation is fine, though later versions we will have the sawmill instead subtract from the player's log count to create its planks, and do so over a 5 second timer instead of a 1 second timer.







We forewent the "List<Upgrade>" as each GameObject stores its upgrade data and updates it on its own, thus not requiring an external manager to keep track of. This is done through the use of custom classes, which greatly speeds up how easy it is to implement new passive upgrades.





