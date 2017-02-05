using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using Lottie;
using UIKit;

namespace Naxam.LottieDemo.iOS
{
	public class AnimatedTextField : UIView, IUICollectionViewDelegate, IUICollectionViewDataSource
	{
		UICollectionView collectionView;
		UICollectionViewLayout layout;
		int fontSize;
		string text;
		bool updatingCells;
		List<CGSize> letterSizes;

		[Export("initWithFrame:")]
		public AnimatedTextField(CGRect frame) : base(frame)
		{
			layout = new UICollectionViewFlowLayout();
			fontSize = 36;
			collectionView = new UICollectionView(Bounds, layout);
			collectionView.RegisterClassForCell(typeof(AnimatedCharacterCell), "char");
			collectionView.Delegate = this;
			collectionView.DataSource = this;
			collectionView.BackgroundColor = UIColor.White;

			AddSubview(collectionView);
		}

		public int FontSize
		{
			get { return fontSize; }
			set
			{
				fontSize = value;

				CalculateLetterSizes();

				layout.InvalidateLayout();
			}
		}

		public string Text
		{
			get { return text; }
			set
			{
				if (string.Equals(text, value))
				{
					return;
				}

				text = value;

				CalculateLetterSizes();
				collectionView.ReloadData();
				ScrollToBottom();
			}
		}

		public UIEdgeInsets ScrollInsets
		{
			get { return collectionView.ContentInset; }
			set
			{
				collectionView.ContentInset = value;
				ScrollToBottom();
			}
		}

		public void ChangeCharactersInRange(AnimatedTextField instance, NSRange range, string replacement)
		{
			NSMutableString newText = new NSMutableString(text.Length);
			newText.Append(new NSString(text));

			if (range.Location > 0)
			{
				newText.ReplaceCharactersInRange(range, new NSString(replacement));
			}

			List<NSIndexPath> updateIndices = new List<NSIndexPath>();
			List<NSIndexPath> addIndices = new List<NSIndexPath>();
			List<NSIndexPath> removeIndices = new List<NSIndexPath>();

			for (nint i = range.Location; i < newText.Length; i++)
			{
				if (i < text.Length)
				{
					updateIndices.Add(NSIndexPath.FromItemSection(i, 0));
				}
				else {
					addIndices.Add(NSIndexPath.FromItemSection(i, 0));
				}
			}

			for (nint i = newText.Length; i < text.Length; i++)
			{
				removeIndices.Add(NSIndexPath.FromItemSection(i, 0));
			}

			updatingCells = true;
			text = newText.ToString();

			CalculateLetterSizes();

			collectionView.PerformBatchUpdates(delegate
			{
				if (addIndices.Count > 0)
				{
					collectionView.InsertItems(addIndices.ToArray());
				}
				if (updateIndices.Count > 0)
				{
					collectionView.ReloadItems(updateIndices.ToArray());
				}
				if (removeIndices.Count > 0)
				{
					collectionView.DeleteItems(removeIndices.ToArray());
				}
			}, (finished) =>
			{
				updatingCells = false;
			});

			ScrollToBottom();
		}

		public nint GetItemsCount(UICollectionView collectionView, nint section)
		{
			var count = (text?.Length ?? 0) + 1;

			return count;
		}

		public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = collectionView.DequeueReusableCell("char", indexPath) as AnimatedCharacterCell;

			return cell;
		}

		[Export("collectionView:willDisplayCell:forItemAtIndexPath:")]
		public void WillDisplayCell(UICollectionView collectionView, UICollectionViewCell cell, NSIndexPath indexPath)
		{
			var xcell = cell as AnimatedCharacterCell;

			if (indexPath.Row < text.Length)
			{
				xcell.SetCharacter(text.Substring(indexPath.Row, 1));
				xcell.DisplayCharacter(updatingCells);
			}
			else {
				xcell.SetCharacter(@"BlinkingCursor");
				xcell.LoopAnimation();
				xcell.DisplayCharacter(true);
			}
		}

		[Export("collectionView:layout:sizeForItemAtIndexPath:")]
		public CGSize SizeForItemAtIndexPath(UICollectionView collectionView, UICollectionViewLayout collectionViewLayout, NSIndexPath indexPath)
		{
			return letterSizes?[indexPath.Row] ?? CGSize.Empty;
		}

		[Export("collectionView:layout:minimumInteritemSpacingForSectionAtIndex:")]
		public nfloat MinimumInteritemSpacingForSectionAtIndex(UICollectionView collectionView, UICollectionViewLayout collectionViewLayout, nint section)
		{
			return 0;
		}

		[Export("collectionView:layout:minimumLineSpacingForSectionAtIndex:")]
		public nfloat MinimumLineSpacingForSectionAtIndex(UICollectionView collectionView, UICollectionViewLayout collectionViewLayout, nint section)
		{
			return 0;
		}

		void ScrollToBottom()
		{
			var bottomOffset = new CGPoint(
				0,
				collectionView.ContentSize.Height - collectionView.Bounds.Size.Height + collectionView.ContentInset.Bottom
			);
			bottomOffset.Y = (nfloat)Math.Max(bottomOffset.Y, 0);

			collectionView.SetContentOffset(bottomOffset, true);
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			collectionView.Frame = Bounds;
		}

		void CalculateLetterSizes()
		{
			var sizes = new List<CGSize>();

			var width = Bounds.Size.Width;
			nfloat currentWidth = 0;

			for (int i = 0; i < text.Length; i++)
			{
				var letter = text.Substring(i, 1).ToUpper();
				var letterSize = SizeOfString(letter);

				if (letter == " " && i + 1 < text.Length)
				{
					var cutString = text.Substring(i + 1);
					var words = cutString.Split(' ');
					if (words.Length > 0)
					{
						var nextWordLength = SizeOfString(words[0]);

						if (currentWidth + nextWordLength.Width + letterSize.Width > width)
						{
							letterSize.Width = (nfloat)Math.Floor(width - currentWidth);
							currentWidth = 0;
						}
						else {
							currentWidth += letterSize.Width;
						}
					}
				}
				else {
					currentWidth += letterSize.Width;
					if (currentWidth >= width)
					{
						currentWidth = letterSize.Width;
					}
				}

				sizes.Add(letterSize);
			}

			sizes.Add(SizeOfString("W"));
			letterSizes = sizes;
		}

		CGSize SizeOfString(string xtext)
		{
			var constaints = new CGSize(1000, 1000);
			var attributes = new NSDictionary(UIStringAttributeKey.Font, UIFont.BoldSystemFontOfSize(FontSize));
			var xstring = new NSString(xtext.ToUpper());
			var textSize = xstring.GetBoundingRect(
				constaints,
				NSStringDrawingOptions.UsesLineFragmentOrigin,
				new UIStringAttributes(attributes),
				null
			).Size;

			textSize.Width += (xstring.Length * 2);

			return textSize;
		}
	}

	public class AnimatedCharacterCell : UICollectionViewCell
	{
		[Export("initWithFrame:")]
		public AnimatedCharacterCell(CGRect frame) : base (frame)
		{

		}

		LAAnimationView vwAnimation;
		string character;

		public void SetCharacter(string character)
		{
			var sanatizedCharacter = character.Substring(0, 1).ToUpper();

			var valid = NSCharacterSet.Letters.Contains(sanatizedCharacter[0]);

			if (character == "BlinkingCursor")
			{
				sanatizedCharacter = character;
			}
			else if (sanatizedCharacter == ",")
			{
				sanatizedCharacter = @"Comma";
				valid = true;
			}
			else if (sanatizedCharacter == @"'")
			{
				sanatizedCharacter = @"Apostrophe";
				valid = true;
			}
			else if (sanatizedCharacter == @":")
			{
				sanatizedCharacter = @"Colon";
				valid = true;
			}
			else if (sanatizedCharacter == @"?")
			{
				sanatizedCharacter = @"QuestionMark";
				valid = true;
			}
			else if (sanatizedCharacter == @"!")
			{
				sanatizedCharacter = @"ExclamationMark";
				valid = true;
			}
			else if (sanatizedCharacter == @".")
			{
				sanatizedCharacter = @"Period";
				valid = true;
			}

			if (sanatizedCharacter == this.character)
			{
				return;
			}

			vwAnimation?.RemoveFromSuperview();
			character = null;
			vwAnimation = null;

			if (!valid)
			{
				return;
			}

			vwAnimation = LAAnimationViewHelper.AnimationFromJsonFileInSubfolder(sanatizedCharacter, "TypeFace");

			if (vwAnimation == null) {
				return;
			}

			vwAnimation.ContentMode = UIViewContentMode.ScaleAspectFit;
			character = sanatizedCharacter;

			ContentView.AddSubview(vwAnimation);

			ArrangeAnimationView();
		}

		public void LoopAnimation()
		{
			if (vwAnimation == null)
			{
				return;
			}

			vwAnimation.LoopAnimation = true;
		}

		public void DisplayCharacter(bool animated)
		{
			if (vwAnimation == null)
			{
				return;
			}

			if (animated)
			{
				vwAnimation.Play();

				return;
			}

			if (vwAnimation.AnimationProgress != 1)
			{
				NSOperationQueue.MainQueue.AddOperation(delegate
				{
					vwAnimation.AnimationProgress = 1;
				});
			}
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			ArrangeAnimationView();
		}

		public override void PrepareForReuse()
		{
			base.PrepareForReuse();
		}

		void ArrangeAnimationView()
		{
			if (vwAnimation == null)
			{
				return;
			}

			var rootBounds = ContentView.Bounds;

			vwAnimation.Frame = new CGRect(rootBounds.Size.Width * -1, 0, rootBounds.Size.Width * 3, rootBounds.Size.Height);
		}
	}
}
