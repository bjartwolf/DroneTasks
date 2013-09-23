<Query Kind="Program">
  <Reference Relative="..\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Client.dll">&lt;MyDocuments&gt;\GitHub\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Client.dll</Reference>
  <Reference Relative="..\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Data.dll">&lt;MyDocuments&gt;\GitHub\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Data.dll</Reference>
  <Reference Relative="..\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Infrastructure.dll">&lt;MyDocuments&gt;\GitHub\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Infrastructure.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <NuGetReference>Rx-Main</NuGetReference>
  <NuGetReference>Rx-Testing</NuGetReference>
  <Namespace>AR.Drone.Client</Namespace>
  <Namespace>AR.Drone.Client.Navigation</Namespace>
  <Namespace>AR.Drone.Data</Namespace>
  <Namespace>AR.Drone.Data.Navigation</Namespace>
  <Namespace>Microsoft.Reactive.Testing</Namespace>
  <Namespace>System</Namespace>
  <Namespace>System.Reactive</Namespace>
  <Namespace>System.Reactive.Concurrency</Namespace>
  <Namespace>System.Reactive.Disposables</Namespace>
  <Namespace>System.Reactive.Subjects</Namespace>
  <Namespace>System.Reactive.Threading.Tasks</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async void Main()
{
	var client = new DroneClient();
	client.ResetEmergency();

	client.Start();
//	var scheduler = new TestScheduler();
	Console.WriteLine("Take off!");
	client.AckControlAndWaitForConfirmation().Dump();
	//	client.NavigationDataAcquiredProper += (obj, args) => args.Dump();
//	var takeoffComplete = client.TakeoffAndHover(scheduler);
	var takeoffComplete = client.TakeoffAndHover();
//	client.SimulateHover();
	Console.WriteLine("We have started the takeoff sequence");
//	scheduler.AdvanceBy(TimeSpan.FromSeconds(12).Ticks);
	var takeoffSuccess = await takeoffComplete;
	if (takeoffSuccess) {
		Console.WriteLine("It worked. We should land");
		client.Land();
	} else {
		Console.WriteLine("I suppose we might have crashed and burned.");
		client.Land(); // Attempt landing anyway...	
	}
}

