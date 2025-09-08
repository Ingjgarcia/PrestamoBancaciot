export async function apiFetch<T>(path: string, init?: RequestInit, authToken?: string): Promise<T> {
  const headers = new Headers(init?.headers || {});
  headers.set("Content-Type", "application/json");
  if (authToken) headers.set("Authorization", `Bearer ${authToken}`);

  const URLBASE = "https://localhost:7076/";
  const res = await fetch(new URL(path, URLBASE).toString(), {
    ...init,
    headers,
  });

  if (!res.ok) {
    const text = await res.text().catch(() => "");
    throw new Error(text || `HTTP ${res.status}`);
  }
  return res.json() as Promise<T>;
}