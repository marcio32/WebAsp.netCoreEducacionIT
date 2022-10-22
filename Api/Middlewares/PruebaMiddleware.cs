namespace Api.Middlewares
{
	public class PruebaMiddleware
	{

		private readonly RequestDelegate next;
		public PruebaMiddleware(RequestDelegate next)
		{
			this.next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			await context.Response.WriteAsync("Primera Linea");
			await next.Invoke(context);
            await context.Response.WriteAsync("Tercera Linea");

        }
	}
}
