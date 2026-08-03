using FrooxEngine;

using HarmonyLib;

using ResoniteModLoader;

namespace PhysicalGrabOnDesktop;

public class PhysicalGrabOnDesktop : ResoniteMod {
	internal const string VERSION_CONSTANT = "1.0.0";
	public override string Name => "PhysicalGrabOnDesktop";
	public override string Author => "Delta";
	public override string Version => VERSION_CONSTANT;
	public override string Link => "https://github.com/XDelta/PhysicalGrabOnDesktop/";

	public override void OnEngineInit() {
		Harmony harmony = new("net.deltawolf.PhysicalGrabOnDesktop");
		harmony.PatchAll();
	}

	[HarmonyPatch(typeof(InteractionHandler), "TryPointGrab")]
	static class TryPointGrabPatch {
		private static readonly Func<InteractionHandler, bool, ICollider?, bool> Grab =
			AccessTools.MethodDelegate<Func<InteractionHandler, bool, ICollider?, bool>>(
			AccessTools.Method(
				typeof(InteractionHandler),
				"Grab",
				[typeof(bool), typeof(ICollider)]));

		static bool Prefix(InteractionHandler __instance) {
			if (Grab(__instance, false, null)) {
				return false;
			}
			return true;
		}
	}
}
