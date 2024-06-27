import './globals.css';
import NavBar from './nav/NavBar';
import SignalRProvider from './providers/SignalRProvider';

export const metadata = {
  title: 'Fashion Place',
  description: 'online store',
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  return (
    <html lang='en'>
      <body>
        <NavBar />
        <main className='container mx-auto px-5 pt-10'>
          <SignalRProvider children={children} />
        </main>
      </body>
    </html>
  );
}
