using System;
using Foundation;
using UIKit;

namespace Lottie
{
	// The first step to creating a binding is to add your native library ("libNativeLibrary.a")
	// to the project by right-clicking (or Control-clicking) the folder containing this source
	// file and clicking "Add files..." and then simply select the native library (or libraries)
	// that you want to bind.
	//
	// When you do that, you'll notice that MonoDevelop generates a code-behind file for each
	// native library which will contain a [LinkWith] attribute. MonoDevelop auto-detects the
	// architectures that the native library supports and fills in that information for you,
	// however, it cannot auto-detect any Frameworks or other system libraries that the
	// native library may depend on, so you'll need to fill in that information yourself.
	//
	// Once you've done that, you're ready to move on to binding the API...
	//
	//
	// Here is where you'd define your API definition for the native Objective-C library.
	//
	// For example, to bind the following Objective-C class:
	//
	//     @interface Widget : NSObject {
	//     }
	//
	// The C# binding would look like this:
	//
	//     [BaseType (typeof (NSObject))]
	//     interface Widget {
	//     }
	//
	// To bind Objective-C properties, such as:
	//
	//     @property (nonatomic, readwrite, assign) CGPoint center;
	//
	// You would add a property definition in the C# interface like so:
	//
	//     [Export ("center")]
	//     CGPoint Center { get; set; }
	//
	// To bind an Objective-C method, such as:
	//
	//     -(void) doSomething:(NSObject *)object atIndex:(NSInteger)index;
	//
	// You would add a method definition to the C# interface like so:
	//
	//     [Export ("doSomething:atIndex:")]
	//     void DoSomething (NSObject object, int index);
	//
	// Objective-C "constructors" such as:
	//
	//     -(id)initWithElmo:(ElmoMuppet *)elmo;
	//
	// Can be bound as:
	//
	//     [Export ("initWithElmo:")]
	//     IntPtr Constructor (ElmoMuppet elmo);
	//
	// For more information, see http://developer.xamarin.com/guides/ios/advanced_topics/binding_objective-c/
	//
	// @interface LAAnimationTransitionController : NSObject <UIViewControllerAnimatedTransitioning>

	[BaseType(typeof(NSObject))]
	interface LAAnimationTransitionController : IUIViewControllerAnimatedTransitioning
	{
		// -(instancetype)initWithAnimationNamed:(NSString *)animation fromLayerNamed:(NSString *)fromLayer toLayerNamed:(NSString *)toLayer;
		[Export("initWithAnimationNamed:fromLayerNamed:toLayerNamed:")]
		IntPtr Constructor(string animation, string fromLayer, string toLayer);
	}

	// typedef void (^LAAnimationCompletionBlock)(BOOL);
	delegate void LAAnimationCompletionBlock(bool arg0);

	// @interface LAAnimationView : UIView
	[BaseType(typeof(UIView))]
	interface LAAnimationView
	{
		// +(instancetype)animationNamed:(NSString *)animationName;
		[Static]
		[Export("animationNamed:")]
		LAAnimationView AnimationNamed(string animationName);

		// +(instancetype)animationFromJSON:(NSDictionary *)animationJSON;
		[Static]
		[Export("animationFromJSON:")]
		LAAnimationView AnimationFromJSON(NSDictionary animationJSON);

		// -(instancetype)initWithContentsOfURL:(NSURL *)url;
		[Export("initWithContentsOfURL:")]
		IntPtr Constructor(NSUrl url);

		// @property (readonly, nonatomic) BOOL isAnimationPlaying;
		[Export("isAnimationPlaying")]
		bool IsAnimationPlaying { get; }

		// @property (assign, nonatomic) BOOL loopAnimation;
		[Export("loopAnimation")]
		bool LoopAnimation { get; set; }

		// @property (assign, nonatomic) CGFloat animationProgress;
		[Export("animationProgress")]
		nfloat AnimationProgress { get; set; }

		// @property (assign, nonatomic) CGFloat animationSpeed;
		[Export("animationSpeed")]
		nfloat AnimationSpeed { get; set; }

		// @property (readonly, nonatomic) CGFloat animationDuration;
		[Export("animationDuration")]
		nfloat AnimationDuration { get; }

		// -(void)playWithCompletion:(LAAnimationCompletionBlock)completion;
		[Export("playWithCompletion:")]
		void PlayWithCompletion(LAAnimationCompletionBlock completion);

		// -(void)play;
		[Export("play")]
		void Play();

		// -(void)pause;
		[Export("pause")]
		void Pause();

		// -(void)addSubview:(UIView *)view toLayerNamed:(NSString *)layer;
		[Export("addSubview:toLayerNamed:")]
		void AddSubview(UIView view, string layer);
	}

	//[Static]
	//[Verify(ConstantsInterfaceAssociation)]
	//partial interface Constants
	//{
	//	// extern double LottieVersionNumber;
	//	[Field("LottieVersionNumber", "__Internal")]
	//	double LottieVersionNumber { get; }

	//	// extern const unsigned char [] LottieVersionString;
	//	[Field("LottieVersionString", "__Internal")]
	//	byte[] LottieVersionString { get; }
	//}
}
