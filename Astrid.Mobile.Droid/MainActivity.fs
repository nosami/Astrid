﻿namespace Astrid.Mobile.Droid

open Android.Content.PM
open Android.App

open Xamarin.Forms.Platform.Android
open Xamarin.Forms.FSharp
open Xamarin.Forms

open ReactiveUI.XamForms
open ReactiveUI

open Splat

open Astrid.Mobile.Shared

type XamarinForms = Xamarin.Forms.Forms

type IAstridPlatform = inherit IPlatform

type Resources = Astrid.Mobile.Droid.Resource

type DroidPlatform() =
    interface IAstridPlatform with
        member __.GetMainPage() = new RoutedViewHost() :> Page

[<Activity (Label = "Astrid.Mobile.Droid", MainLauncher = true, ConfigurationChanges = (ConfigChanges.ScreenSize ||| ConfigChanges.Orientation))>]
type MainActivity () =
    inherit FormsApplicationActivity ()
    override this.OnCreate (bundle) =
        base.OnCreate(bundle)
        XamarinForms.Init(this, bundle)
        let platform = new DroidPlatform() :> IAstridPlatform
        let application = new App<IAstridPlatform>(platform, new UiContext(this), new Configuration())
        this.LoadApplication(application)
        Locator.Current.GetService<IScreen>().Router.Navigate.Execute(new DashboardViewModel()) |> ignore



