namespace Wholemy {
	public class Mat {
		#region #method# Sqrt(X, Y) 
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public static decimal Sqrt(decimal X, decimal Y) {
			X = Y = X * X + Y * Y;
			var R = X * 0.5m;
			while(Y != R) { Y = R; R = (R + (X / R)) * 0.5m; }
			return R;
		}
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public static double Sqrt(double X, double Y) {
			#region #debug# 
#if DEBUG
			if(double.IsNaN(X) || double.IsNaN(Y)) throw new System.ArgumentOutOfRangeException();
#endif
			#endregion
			X = Y = X * X + Y * Y;
			var R = X * 0.5;
			while(Y != R) { Y = R; R = (R + (X / R)) * 0.5; }
			return R;
		}
		#region #through# 
#if TRACE
		[System.Diagnostics.DebuggerStepThrough]
#endif
		#endregion
		public static float Sqrt(float X, float Y) {
			#region #debug# 
#if DEBUG
			if(float.IsNaN(X) || float.IsNaN(Y)) throw new System.ArgumentOutOfRangeException();
#endif
			#endregion
			X = Y = X * X + Y * Y;
			var R = X * 0.5f;
			while(Y != R) { Y = R; R = (R + (X / R)) * 0.5f; }
			return R;
		}
		#endregion
		#region #method# GetAR(CX, CY, BX, BY, AX, AY) 
		/// <summary>Возвращает корень поворота от 0.0 до 4.0)</summary>
		/// <param name="CX">Центр по оси X)</param>
		/// <param name="CY">Центр по оси Y)</param>
		/// <param name="BX">Старт по оси X)</param>
		/// <param name="BY">Старт по оси Y)</param>
		/// <param name="AX">Конец по оси X)</param>
		/// <param name="AY">Конец по оси Y)</param>
		/// <returns>Возвращает корень поворота от 0.0 до 4.0)</returns>
		/// <exception cref="System.InvalidProgramException">
		/// Возникает в случае непредусмотренного состояния, требует исправления)</exception>
		public static float GetAR(float CX, float CY, float BX, float BY, float AX, float AY) {
			var BL = Sqrt(CX - BX, CY - BY);
			if(BL == 0.0f) return 0.0f;
			var AL = Sqrt(CX - AX, CY - AY);
			if(AL == 0.0f) return 0.0f;
			AX = CX + (AX - CX) / AL * BL;
			AY = CY + (AY - CY) / AL * BL;
			AL = Sqrt(CX - AX, CY - AY);
			var X1 = CY - BY + CX; var Y1 = BX - CX + CY; // 90
			var X2 = CX - BX + CX; var Y2 = CY - BY + CY; // 180
			var X3 = BY - CY + CX; var Y3 = CX - BX + CY; // 270
			var L0 = Sqrt(BX - AX, BY - AY);
			var L1 = Sqrt(X1 - AX, Y1 - AY);
			var L2 = Sqrt(X2 - AX, Y2 - AY);
			var L3 = Sqrt(X3 - AX, Y3 - AY);
			float R = 0.0f, MX = 0.0f, MY = 0.0f, EX = 0.0f, EY = 0.0f;
			if(L0 < L2 && L0 < L3 && L1 < L2 && L1 <= L3) {
				R = 0.0f; MX = BX; MY = BY; EX = X1; EY = Y1;
			} else if(L1 < L3 && L1 < L0 && L2 < L3 && L2 <= L0) {
				R = 1.0f; MX = X1; MY = Y1; EX = X2; EY = Y2; L0 = L1; L1 = L2;
			} else if(L2 < L0 && L2 < L1 && L3 < L0 && L3 <= L1) {
				R = 2.0f; MX = X2; MY = Y2; EX = X3; EY = Y3; L0 = L2; L1 = L3;
			} else if(L3 < L1 && L3 < L2 && L0 < L1 && L0 <= L2) {
				R = 3.0f; MX = X3; MY = Y3; EX = BX; EY = BY; L1 = L0; L0 = L3;
			} else { throw new System.InvalidProgramException(); }
			var AR = 1.0f;
			while(L0 > 0.0f && (L2 = Sqrt(MX - EX, MY - EY)) > 0.0f) {
				AR *= 0.5f;
				L3 = L2 * 0.5f;
				BX = MX + (EX - MX) / L2 * L3;
				BY = MY + (EY - MY) / L2 * L3;
				L2 = Sqrt(CX - BX, CY - BY);
				BX = CX + (BX - CX) / L2 * BL;
				BY = CY + (BY - CY) / L2 * BL;
				L3 = Sqrt(AX - BX, AY - BY);
				if(L0 < L1) {
					if(EX == BX && EY == BY) break; if(L1 <= L3) break;
					EX = BX; EY = BY; L1 = L3;
				} else {
					if(MX == BX && MY == BY) break; if(L0 <= L3) break;
					MX = BX; MY = BY; L0 = L3; R += AR;
				}
			}
			return R;
		}
		/// <summary>Возвращает корень поворота от 0.0 до 4.0)</summary>
		/// <param name="CX">Центр по оси X)</param>
		/// <param name="CY">Центр по оси Y)</param>
		/// <param name="BX">Старт по оси X)</param>
		/// <param name="BY">Старт по оси Y)</param>
		/// <param name="AX">Конец по оси X)</param>
		/// <param name="AY">Конец по оси Y)</param>
		/// <returns>Возвращает корень поворота от 0.0 до 4.0)</returns>
		/// <exception cref="System.InvalidProgramException">
		/// Возникает в случае непредусмотренного состояния, требует исправления)</exception>
		public static double GetAR(double CX, double CY, double BX, double BY, double AX, double AY) {
			var BL = Sqrt(CX - BX, CY - BY);
			if(BL == 0.0) return 0.0;
			var AL = Sqrt(CX - AX, CY - AY);
			if(AL == 0.0) return 0.0;
			AX = CX + (AX - CX) / AL * BL;
			AY = CY + (AY - CY) / AL * BL;
			AL = Sqrt(CX - AX, CY - AY);
			var X1 = CY - BY + CX; var Y1 = BX - CX + CY; // 90
			var X2 = CX - BX + CX; var Y2 = CY - BY + CY; // 180
			var X3 = BY - CY + CX; var Y3 = CX - BX + CY; // 270
			var L0 = Sqrt(BX - AX, BY - AY);
			var L1 = Sqrt(X1 - AX, Y1 - AY);
			var L2 = Sqrt(X2 - AX, Y2 - AY);
			var L3 = Sqrt(X3 - AX, Y3 - AY);
			double R = 0.0, MX = 0.0, MY = 0.0, EX = 0.0, EY = 0.0;
			if(L0 < L2 && L0 < L3 && L1 < L2 && L1 <= L3) {
				R = 0.0; MX = BX; MY = BY; EX = X1; EY = Y1;
			} else if(L1 < L3 && L1 < L0 && L2 < L3 && L2 <= L0) {
				R = 1.0; MX = X1; MY = Y1; EX = X2; EY = Y2; L0 = L1; L1 = L2;
			} else if(L2 < L0 && L2 < L1 && L3 < L0 && L3 <= L1) {
				R = 2.0; MX = X2; MY = Y2; EX = X3; EY = Y3; L0 = L2; L1 = L3;
			} else if(L3 < L1 && L3 < L2 && L0 < L1 && L0 <= L2) {
				R = 3.0; MX = X3; MY = Y3; EX = BX; EY = BY; L1 = L0; L0 = L3;
			} else { throw new System.InvalidProgramException(); }
			var AR = 1.0;
			while(L0 > 0.0 && (L2 = Sqrt(MX - EX, MY - EY)) > 0.0) {
				AR *= 0.5;
				L3 = L2 * 0.5;
				BX = MX + (EX - MX) / L2 * L3;
				BY = MY + (EY - MY) / L2 * L3;
				L2 = Sqrt(CX - BX, CY - BY);
				BX = CX + (BX - CX) / L2 * BL;
				BY = CY + (BY - CY) / L2 * BL;
				L3 = Sqrt(AX - BX, AY - BY);
				if(L0 < L1) {
					if(EX == BX && EY == BY) break; if(L1 <= L3) break;
					EX = BX; EY = BY; L1 = L3;
				} else {
					if(MX == BX && MY == BY) break; if(L0 <= L3) break;
					MX = BX; MY = BY; L0 = L3; R += AR;
				}
			}
			return R;
		}
		/// <summary>Возвращает корень поворота от 0.0 до 4.0)</summary>
		/// <param name="CX">Центр по оси X)</param>
		/// <param name="CY">Центр по оси Y)</param>
		/// <param name="BX">Старт по оси X)</param>
		/// <param name="BY">Старт по оси Y)</param>
		/// <param name="AX">Конец по оси X)</param>
		/// <param name="AY">Конец по оси Y)</param>
		/// <returns>Возвращает корень поворота от 0.0 до 4.0)</returns>
		/// <exception cref="System.InvalidProgramException">
		/// Возникает в случае непредусмотренного состояния, требует исправления)</exception>
		public static decimal GetAR(decimal CX, decimal CY, decimal BX, decimal BY, decimal AX, decimal AY) {
			var BL = Sqrt(CX - BX, CY - BY);
			if(BL == 0.0m) return 0.0m;
			var AL = Sqrt(CX - AX, CY - AY);
			if(AL == 0.0m) return 0.0m;
			AX = CX + (AX - CX) / AL * BL;
			AY = CY + (AY - CY) / AL * BL;
			AL = Sqrt(CX - AX, CY - AY);
			var X1 = CY - BY + CX; var Y1 = BX - CX + CY; // 90
			var X2 = CX - BX + CX; var Y2 = CY - BY + CY; // 180
			var X3 = BY - CY + CX; var Y3 = CX - BX + CY; // 270
			var L0 = Sqrt(BX - AX, BY - AY);
			var L1 = Sqrt(X1 - AX, Y1 - AY);
			var L2 = Sqrt(X2 - AX, Y2 - AY);
			var L3 = Sqrt(X3 - AX, Y3 - AY);
			decimal R = 0.0m, MX = 0.0m, MY = 0.0m, EX = 0.0m, EY = 0.0m;
			if(L0 < L2 && L0 < L3 && L1 < L2 && L1 <= L3) {
				R = 0.0m; MX = BX; MY = BY; EX = X1; EY = Y1;
			} else if(L1 < L3 && L1 < L0 && L2 < L3 && L2 <= L0) {
				R = 1.0m; MX = X1; MY = Y1; EX = X2; EY = Y2; L0 = L1; L1 = L2;
			} else if(L2 < L0 && L2 < L1 && L3 < L0 && L3 <= L1) {
				R = 2.0m; MX = X2; MY = Y2; EX = X3; EY = Y3; L0 = L2; L1 = L3;
			} else if(L3 < L1 && L3 < L2 && L0 < L1 && L0 <= L2) {
				R = 3.0m; MX = X3; MY = Y3; EX = BX; EY = BY; L1 = L0; L0 = L3;
			} else { throw new System.InvalidProgramException(); }
			var AR = 1.0m;
			while(L0 > 0.0m && (L2 = Sqrt(MX - EX, MY - EY)) > 0.0m) {
				AR *= 0.5m;
				L3 = L2 * 0.5m;
				BX = MX + (EX - MX) / L2 * L3;
				BY = MY + (EY - MY) / L2 * L3;
				L2 = Sqrt(CX - BX, CY - BY);
				BX = CX + (BX - CX) / L2 * BL;
				BY = CY + (BY - CY) / L2 * BL;
				L3 = Sqrt(AX - BX, AY - BY);
				if(L0 < L1) {
					if(EX == BX && EY == BY) break; if(L1 <= L3) break;
					EX = BX; EY = BY; L1 = L3;
				} else {
					if(MX == BX && MY == BY) break; if(L0 <= L3) break;
					MX = BX; MY = BY; L0 = L3; R += AR;
				}
			}
			return R;
		}
		#endregion
		#region #method# Rotate(CX, CY, BX, BY, AR) 
		/// <summary>Поворачивает координаты #float# вокруг центра по корню четверти круга
		/// где 90 градусов равно значению 1.0 а 360 градусов равно значению 4.0)</summary>
		/// <param name="CX">Центр по оси X)</param>
		/// <param name="CY">Центр по оси Y)</param>
		/// <param name="BX">Старт и возвращаемый результат поворота по оси X)</param>
		/// <param name="BY">Старт и возвращаемый результат поворота по оси Y)</param>
		/// <param name="AR">Корень четверти от 0.0 до 4.0 отрицательная в обратную сторону)</param>
		public static bool Rotate(float CX, float CY, ref float BX, ref float BY, float AR) {
			if(AR == 0.0f) return false;
			var Len = Sqrt(CX - BX, CY - BY);
			if(Len == 0.0f) return false;
			var R = (int)AR;
			if(AR < 0.0) { AR = 1.0f + (AR - R); R %= 4; R += 3; } else { AR -= R; R %= 4; }
			var MX = BX; var MY = BY;
			if(R == 1) { MX = CY - BY + CX; MY = BX - CX + CY; } // 90
			else if(R == 2) { MX = CX - BX + CX; MY = CY - BY + CY; } // 180
			else if(R == 3) { MX = BY - CY + CX; MY = CX - BX + CY; } // 270
			var EX = BX; var EY = BY; BX = MX; BY = MY;
			if(AR > 0.0f && R >= 0 && R < 3) { EX = CY - MY + CX; EY = MX - CX + CY; } // 90
			while(AR > 0.0f && AR < 1.0f) {
				var L = Sqrt(MX - EX, MY - EY);
				if(L == 0.0f) break;
				var ll = L / 2f;
				if(AR < 0.5f) {
					EX = MX + (EX - MX) / L * ll; EY = MY + (EY - MY) / L * ll;
					ll = Sqrt(CX - EX, CY - EY);
					EX = CX + (EX - CX) / ll * Len; EY = CY + (EY - CY) / ll * Len;
					AR = AR * 2.0f; BX = EX; BY = EY;
				} else {
					MX = EX + (MX - EX) / L * ll; MY = EY + (MY - EY) / L * ll;
					ll = Sqrt(CX - MX, CY - MY);
					MX = CX + (MX - CX) / ll * Len; MY = CY + (MY - CY) / ll * Len;
					AR = (AR - 0.5f) * 2.0f; BX = MX; BY = MY;
				}
			}
			return true;
		}
		/// <summary>Поворачивает координаты #double# вокруг центра по корню четверти круга
		/// где 90 градусов равно значению 1.0 а 360 градусов равно значению 4.0)</summary>
		/// <param name="CX">Центр по оси X)</param>
		/// <param name="CY">Центр по оси Y)</param>
		/// <param name="BX">Старт и возвращаемый результат поворота по оси X)</param>
		/// <param name="BY">Старт и возвращаемый результат поворота по оси Y)</param>
		/// <param name="AR">Корень четверти от 0.0 до 4.0 отрицательная в обратную сторону)</param>
		public static bool Rotate(double CX, double CY, ref double BX, ref double BY, double AR) {
			if(AR == 0.0) return false;
			var Len = Sqrt(CX - BX, CY - BY);
			if(Len == 0.0) return false;
			var R = (long)AR;
			if(AR < 0.0) { AR = 1.0 + (AR - R); R %= 4; R += 3; } else { AR -= R; R %= 4; }
			var MX = BX; var MY = BY;
			if(R == 1) { MX = CY - BY + CX; MY = BX - CX + CY; } // 90
			else if(R == 2) { MX = CX - BX + CX; MY = CY - BY + CY; } // 180
			else if(R == 3) { MX = BY - CY + CX; MY = CX - BX + CY; } // 270
			var EX = BX; var EY = BY; BX = MX; BY = MY;
			if(AR > 0.0 && R >= 0 && R < 3) { EX = CY - MY + CX; EY = MX - CX + CY; } // 90
			while(AR > 0.0 && AR < 1.0) {
				var L = Sqrt(MX - EX, MY - EY);
				if(L == 0.0) break;
				var ll = L / 2;
				if(AR < 0.5) {
					EX = MX + (EX - MX) / L * ll; EY = MY + (EY - MY) / L * ll;
					ll = Sqrt(CX - EX, CY - EY);
					EX = CX + (EX - CX) / ll * Len; EY = CY + (EY - CY) / ll * Len;
					AR = AR * 2.0; BX = EX; BY = EY;
				} else {
					MX = EX + (MX - EX) / L * ll; MY = EY + (MY - EY) / L * ll;
					ll = Sqrt(CX - MX, CY - MY);
					MX = CX + (MX - CX) / ll * Len; MY = CY + (MY - CY) / ll * Len;
					AR = (AR - 0.5) * 2.0; BX = MX; BY = MY;
				}
			}
			return true;
		}
		/// <summary>Поворачивает координаты #decimal# вокруг центра по корню четверти круга
		/// где 90 градусов равно значению 1.0 а 360 градусов равно значению 4.0)</summary>
		/// <param name="CX">Центр по оси X)</param>
		/// <param name="CY">Центр по оси Y)</param>
		/// <param name="BX">Старт и возвращаемый результат поворота по оси X)</param>
		/// <param name="BY">Старт и возвращаемый результат поворота по оси Y)</param>
		/// <param name="AR">Корень четверти от 0.0 до 4.0 отрицательная в обратную сторону)</param>
		public static bool Rotate(decimal CX, decimal CY, ref decimal BX, ref decimal BY, decimal AR) {
			if(AR == 0.0m) return false;
			var Len = Sqrt(CX - BX, CY - BY);
			if(Len == 0.0m) return false;
			var R = (long)AR;
			if(AR < 0.0m) { AR = 1.0m + (AR - R); R %= 4; R += 3; } else { AR -= R; R %= 4; }
			var MX = BX; var MY = BY;
			if(R == 1) { MX = CY - BY + CX; MY = BX - CX + CY; } // 90
			else if(R == 2) { MX = CX - BX + CX; MY = CY - BY + CY; } // 180
			else if(R == 3) { MX = BY - CY + CX; MY = CX - BX + CY; } // 270
			var EX = BX; var EY = BY; BX = MX; BY = MY;
			if(AR > 0.0m && R >= 0 && R < 3) { EX = CY - MY + CX; EY = MX - CX + CY; } // 90
			while(AR > 0.0m && AR < 1.0m) {
				var L = Sqrt(MX - EX, MY - EY);
				if(L == 0.0m) break;
				var ll = L / 2;
				if(AR < 0.5m) {
					EX = MX + (EX - MX) / L * ll; EY = MY + (EY - MY) / L * ll;
					ll = Sqrt(CX - EX, CY - EY);
					EX = CX + (EX - CX) / ll * Len; EY = CY + (EY - CY) / ll * Len;
					AR = AR * 2.0m; BX = EX; BY = EY;
				} else {
					MX = EX + (MX - EX) / L * ll; MY = EY + (MY - EY) / L * ll;
					ll = Sqrt(CX - MX, CY - MY);
					MX = CX + (MX - CX) / ll * Len; MY = CY + (MY - CY) / ll * Len;
					AR = (AR - 0.5m) * 2.0m; BX = MX; BY = MY;
				}
			}
			return true;
		}
		#endregion
	}
}
