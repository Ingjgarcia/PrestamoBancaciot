export { default as authReducer } from "./authSlice"; //exporta el redux
export * from "./authSlice";   // exporta actions 
//exporta los tipos
export * from "../../types/auth/AuthResponse";
export * from "../../types/auth/LoginRequest ";

export { default as AuthView } from "./AuthView";