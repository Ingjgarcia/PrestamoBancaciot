import { createAsyncThunk } from "@reduxjs/toolkit";
import type { Prestamo } from "../../types/Prestamos/Prestamo";
import type { AddPrestamoRequest } from "../../types/Prestamos/AddPrestamoRequest";
import { apiFetch } from "../../app/utils/apiFetch";
import type { RootState } from "../../app/store";

export const addPrestamo = createAsyncThunk<Prestamo, AddPrestamoRequest, { state: RootState }>(
  "prestamos/add",
  async (body, { getState }) => {
    const token = getState().auth.token!;
    return apiFetch<Prestamo>("api/prestamos/Add", {
      method: "POST",
      body: JSON.stringify(body),
    }, token);
  }
);

export const getPrestamosAll = createAsyncThunk<Prestamo[], void, { state: RootState }>(
  "prestamos/getAll",
  async (_, { getState }) => {
    const token = getState().auth.token!;
    return apiFetch<Prestamo[]>("api/prestamos/GetAll", undefined, token);
  }
);

export const getPrestamosPendientes = createAsyncThunk<Prestamo[], void, { state: RootState }>(
  "prestamos/getPendientes",
  async (_, { getState }) => {
    const token = getState().auth.token!;
    return apiFetch<Prestamo[]>("api/prestamos/prestamos-pendientes", undefined, token);
  }
);

export const aprobarPrestamo = createAsyncThunk<Prestamo, string, { state: RootState }>(
  "prestamos/aprobar",
  async (id, { getState }) => {
    const token = getState().auth.token!;
    return apiFetch<Prestamo>(`api/prestamos/${id}/approbar`, { method: "POST" }, token);
  }
);

export const rechazarPrestamo = createAsyncThunk<Prestamo, string, { state: RootState }>(
  "prestamos/rechazar",
  async (id, { getState }) => {
    const token = getState().auth.token!;
    return apiFetch<Prestamo>(`api/prestamos/${id}/rechazar`, { method: "POST" }, token);
  }
);