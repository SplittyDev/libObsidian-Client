using System;

namespace libObsidian.Client
{
	/// <summary>
	/// HTTP status.
	/// </summary>
	public enum HttpStatus {

		#region 2xx Success

		/// <summary>
		/// 200 OK
		/// </summary>
		OK					= 200,

		/// <summary>
		/// 201 Created
		/// </summary>
		Created				= 201,

		/// <summary>
		/// 204 No Content
		/// </summary>
		NoContent			= 204,

		#endregion

		#region 3xx Redirection

		/// <summary>
		/// 304 Not Modified
		/// </summary>
		NotModified			= 304,

		#endregion

		#region 4xx Client Error

		/// <summary>
		/// 400 Bad Request
		/// </summary>
		BadRequest			= 400,

		/// <summary>
		/// 401 Unauthorized
		/// </summary>
		Unauthorized		= 401,

		/// <summary>
		/// 403 Forbidden
		/// </summary>
		Forbidden			= 403,

		/// <summary>
		/// 404 Not Found
		/// </summary>
		NotFound			= 404,

		/// <summary>
		/// 409 Conflict
		/// </summary>
		Conflict			= 409,

		#endregion

		#region 5xx Server Error

		/// <summary>
		/// 500 Internal Server Error
		/// </summary>
		InternalServerError	= 500,

		#endregion
	}
}

