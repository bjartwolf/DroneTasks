<Query Kind="Program">
  <Reference Relative="..\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Client.dll">&lt;MyDocuments&gt;\GitHub\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Client.dll</Reference>
  <Reference Relative="..\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Data.dll">&lt;MyDocuments&gt;\GitHub\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Data.dll</Reference>
  <Reference Relative="..\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Infrastructure.dll">&lt;MyDocuments&gt;\GitHub\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Infrastructure.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>AR.Drone.Client</Namespace>
  <Namespace>AR.Drone.Client.Navigation</Namespace>
  <Namespace>AR.Drone.Data</Namespace>
  <Namespace>AR.Drone.Data.Navigation</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async void Main()
{
	var client = new DroneClient();
	Console.WriteLine("9. 8. 7. 6. 5. 4. 3. 2. 1. 0."); // This could have been a cool Rx thingy
	Console.WriteLine("Take off!");
	var takeoffComplete = TakeoffAndHover(client);
	Console.WriteLine("We have started the takeoff sequence");
	if (await takeoffComplete) {
		Console.WriteLine("It worked. We should land");
		client.Land();
	} else {
		Console.WriteLine("I suppose we might have crashed and burned.");
	}
}

Task TakeoffAndHover(DroneClient client) {
	var tcs = new TaskCompletionSource<bool>();
	Action<NavigationData> handler = null;
	handler = (data) => { 
		if (data.State == NavigationState.Hovering) {
			tcs.SetResult(true);
			client.NavigationDataAcquired -= handler;
		} else if (data.State == NavigationState.Emergency) {
			client.NavigationDataAcquired -= handler;
			tcs.SetResult(false);
		}
	};
	
	client.Takeoff();
	var timeout = TimeSpan.FromSeconds(5);
    var ct = new CancellationTokenSource(timeout);
	ct.Token.Register(() => {
		client.NavigationDataAcquired -= handler;
		tcs.SetResult(false);
	});
	return tcs.Task;
}
