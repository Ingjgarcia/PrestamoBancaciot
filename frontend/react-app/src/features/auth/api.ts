import { createAsyncThunk } from "@reduxjs/toolkit";
import { apiFetch } from "../../app/utils/apiFetch";
import type { AuthResponse, LoginRequest } from ".";


export const login = createAsyncThunk<AuthResponse, LoginRequest>(
  "auth/login",
  async (body) =>
    apiFetch<AuthResponse>("api/auth/login", {
      method: "POST",
      body: JSON.stringify(body),
    })
);

export const register = createAsyncThunk<AuthResponse, LoginRequest>(
  "auth/register",
  async (body) =>
    apiFetch<AuthResponse>("api/auth/register", {
      method: "POST",
      body: JSON.stringify(body),
    })
);
