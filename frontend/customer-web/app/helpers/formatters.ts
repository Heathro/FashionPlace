export default function formatPrice(price: number) {
  return price.toLocaleString('en-GB', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
}