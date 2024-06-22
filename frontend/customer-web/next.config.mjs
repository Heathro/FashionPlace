/** @type {import('next').NextConfig} */
const nextConfig = {
  logging: {
    fetches: {
      fullUrl: true
    }
  },
  images: {
    domains: [
      'images.pexels.com'
    ]
  }
};

export default nextConfig;
