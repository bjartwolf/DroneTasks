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
	var scheduler = new TestScheduler();
	Console.WriteLine("Take off!");
	var takeoffComplete = client.TakeoffAndHover(scheduler);
	Console.WriteLine("We have started the takeoff sequence, jump 12 seconds");
	scheduler.AdvanceBy(TimeSpan.FromSeconds(12).Ticks);
	if (await takeoffComplete) {
		Console.WriteLine("It worked. We should land");
		client.Land();
	} else {
		Console.WriteLine("I suppose we might have crashed and burned.");
		client.Land(); // Attempt landing anyway...	
	}
}

//        public Task<bool> TakeoffAndHover(TimeSpan timeout, IScheduler scheduler)
//        {
//            var tcs = new TaskCompletionSource<bool>();
//            IObservable<EventPattern<NavigationData>> navdataStream = Observable.FromEventPattern<NavigationData>(this, "NavigationDataAcquiredProper");
//            var hovering = navdataStream.Where((navdata) => navdata.EventArgs.State == NavigationState.Hovering);
//            var emergency = navdataStream.Where((navdata) => navdata.EventArgs.State == NavigationState.Emergency);
//            var amb = Observable.Amb(hovering, emergency).Timeout(timeout, scheduler);
//            amb.Take(1).Subscribe((navdata) => {
//                if (navdata.EventArgs.State == NavigationState.Hovering)
//                {
//                    tcs.SetResult(true);
//                }
//                else
//                {
//                    tcs.SetResult(false);
//                }
//            }, (ex) => tcs.SetResult(false)); 
//            this.Takeoff();
//            return tcs.Task;
//        }


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