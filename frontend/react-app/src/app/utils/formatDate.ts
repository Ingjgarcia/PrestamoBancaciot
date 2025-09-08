export const formatDate = (input?: string | Date | null): string => {
  if (!input) return ''; // o puedes retornar 'Fecha no disponible'

  const date = new Date(input);
  if (isNaN(date.getTime())) return ''; // fecha inválida

  const day = String(date.getDate()).padStart(2, '0');
  const month = String(date.getMonth() + 1).padStart(2, '0');
  const year = date.getFullYear();

  return `${day}-${month}-${year}`;
};
