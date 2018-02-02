using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GL2 {

    // The standard Unity library seems to be have a bug where MultMatrix replaces modelview instead of multiplying it?
	public static void MultMatrix(Matrix4x4 m) {
        Matrix4x4 newMatrix = GL.modelview * m;
        GL.LoadIdentity();
        GL.MultMatrix(newMatrix);
    }
}
