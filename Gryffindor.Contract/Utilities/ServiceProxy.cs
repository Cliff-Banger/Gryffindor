using System;
using System.ServiceModel;

namespace Gryffindor.Contract.Utilities
{
   public class ServiceProxy<T> : IDisposable where T : class
   {
      private ChannelFactory<T> _factory;
      private T _channel;

      //private readonly object _lockObject = new object();
      private bool _disposed;

      public T Channel
      {
         get
         {
            if (_disposed)
               throw new ObjectDisposedException("Resource ServiceProxy<" + typeof (T) + "> has been disposed");

            //lock (_lockObject)
               if (_factory == null)
               {
                  _factory = new ChannelFactory<T>(typeof(T).Name + "_Endpoint");
                  _channel = _factory.CreateChannel();
               }
            return _channel;
         }
      }

      public void Dispose()
      {
         Dispose(true);
         GC.SuppressFinalize(this);
      }

      public void Dispose(bool disposing)
      {
         if (!_disposed)
            if (disposing)
               //lock (_lockObject)
               {
                  if (_channel != null)
                     ((IClientChannel) _channel).Close();

                  if (_factory != null)
                     _factory.Close();
               }
         _channel = null;
         _factory = null;
         _disposed = true;
      }
   }
}
