import { createSlice } from "@reduxjs/toolkit";
import type { IAuthState } from "../../types/auth/IAuthState";
import { decodeToken } from "../../app/utils/jwt";
import { login, register } from "./api";
import type { AuthResponse } from ".";

const initialState: IAuthState = {
  email: localStorage.getItem("email"),
  token: localStorage.getItem("token"),
  rol: localStorage.getItem("rol"),
  userId: localStorage.getItem("userId"),
  status: "idle",
  error: null,
};


const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    logout: (state) => {
      state.email = null;
      state.token = null;
      state.rol = null;
      state.userId = null;
      localStorage.clear();
    },
  },
  extraReducers: (builder) => {
    const saveUser = (state: IAuthState, payload: AuthResponse) => {
      state.email = payload.email;
      state.token = payload.token;
      state.rol = payload.rol;
      const decoded = decodeToken(payload.token);
      state.userId = decoded.Userid;

      localStorage.setItem("email", payload.email);
      localStorage.setItem("token", payload.token);
      localStorage.setItem("rol", payload.rol);
      localStorage.setItem("userId", decoded.Userid);
    };

    builder
      .addCase(login.pending, (s) => { s.status = "loading"; s.error = null; })
      .addCase(login.fulfilled, (s, a) => { s.status = "succeeded"; saveUser(s, a.payload); })
      .addCase(login.rejected, (s, a) => { s.status = "failed"; s.error = a.error.message || "Error login"; })

      .addCase(register.pending, (s) => { s.status = "loading"; s.error = null; })
      .addCase(register.fulfilled, (s, a) => { s.status = "succeeded"; saveUser(s, a.payload); })
      .addCase(register.rejected, (s, a) => { s.status = "failed"; s.error = a.error.message || "Error registro"; });
  },
});

export const { logout } = authSlice.actions;
export default authSlice.reducer;