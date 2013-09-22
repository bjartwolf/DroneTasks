<Query Kind="Program">
  <Reference Relative="..\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Client.dll">&lt;MyDocuments&gt;\GitHub\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Client.dll</Reference>
  <Reference Relative="..\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Data.dll">&lt;MyDocuments&gt;\GitHub\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Data.dll</Reference>
  <Reference Relative="..\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Infrastructure.dll">&lt;MyDocuments&gt;\GitHub\AR.Drone\AR.Drone.Client\bin\Release\AR.Drone.Infrastructure.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <NuGetReference>Rx-Main</NuGetReference>
  <Namespace>AR.Drone.Client</Namespace>
  <Namespace>AR.Drone.Client.Navigation</Namespace>
  <Namespace>AR.Drone.Data</Namespace>
  <Namespace>AR.Drone.Data.Navigation</Namespace>
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
	Console.WriteLine("9. 8. 7. 6. 5. 4. 3. 2. 1. 0."); // This could have been a cool Rx thingy
	Console.WriteLine("Take off!");
	var takeoffComplete = client.TakeoffAndHover(TimeSpan.FromSeconds(10));
	Console.WriteLine("We have started the takeoff sequence");
	if (await takeoffComplete) {
		Console.WriteLine("It worked. We should land");
		client.Land();
	} else {
		Console.WriteLine("I suppose we might have crashed and burned.");
		client.Land(); // Attempt landing anyway...	
	}
	var timeout = new System.Reactive.O
	
}

//        public Task<bool> TakeoffAndHover(TimeSpan timeout)
//        {
//            var tcs = new TaskCompletionSource<bool>();
//            IObservable<EventPattern<NavigationData>> navdataStream = Observable.FromEventPattern<NavigationData>(this, "NavigationDataAcquiredProper");
//            var hovering = navdataStream.Where((navdata) => navdata.EventArgs.State == NavigationState.Hovering);
//            var emergency = navdataStream.Where((navdata) => navdata.EventArgs.State == NavigationState.Emergency);
//            var amb = Observable.Amb(hovering, emergency);
//            Observable.Timeout(amb,timeout);
//            amb.Take(1).Subscribe((navdata) => {
//                if (navdata.EventArgs.State == NavigationState.Hovering)
//                {
//                    tcs.SetResult(true);
//                }
//                else
//                {
//                    tcs.SetResult(false);
//                }
//            }, () => tcs.SetResult(false)); // Timeout
//            this.Takeoff();
//            return tcs.Task;
//        }
//
//

// 		Had to add the proper event, since the drone api uses actions
//        public event EventHandler<NavigationData> NavigationDataAcquiredProper;
//		private void OnNavigationDataAcquired(NavigationData navigationData)
//        {
//            if (NavigationDataAcquired != null) {
//                NavigationDataAcquired(navigationData);
//                NavigationDataAcquiredProper(this, navigationData);
//                }
//        }
//