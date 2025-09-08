import { configureStore } from "@reduxjs/toolkit";
import authReducer from "../features/auth/authSlice";
import prestamosReducer from "../features/prestamos/prestamosSlice";

export const store = configureStore({
  reducer: {
    auth: authReducer,
    prestamos: prestamosReducer,
  },
});

export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;