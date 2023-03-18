	using Godot;

	public static class Extensions {
		public static SceneController getSceneController(this Node node) {
			return node.GetNode<SceneController>("/root/Main/SceneController");
		}

		public static void removeAllChildren(this Node node) {
			foreach (var child in node.GetChildren()) {
				if (child is Node innerNode) {
					innerNode.QueueFree();
				} 
			}
		}
		
		public static GameWrapper getGameWrapperController(this Node node) {
			return node.GetNode<GameWrapper>("/root/Main/Screen/GameWrapper");
		}
	}
